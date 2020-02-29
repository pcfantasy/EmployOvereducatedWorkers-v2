using ColossalFramework;
using ColossalFramework.Plugins;
using System.Reflection;
using UnityEngine;

namespace EmployOvereducatedWorkers
{
    public class CustomTransferManager : TransferManager
    {
        public static bool _init = false;

        public static void Init()
        {
            var inst = instance;
            var incomingCount = typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance);
            var incomingOffers = typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance);
            var incomingAmount = typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance);
            var outgoingCount = typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.NonPublic | BindingFlags.Instance);
            var outgoingOffers = typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.NonPublic | BindingFlags.Instance);
            var outgoingAmount = typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.NonPublic | BindingFlags.Instance);
            if (inst == null)
            {
                CODebugBase<LogChannel>.Error(LogChannel.Core, "No instance of TransferManager found!");
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, "No instance of TransferManager found!");
                return;
            }
            m_incomingCount = incomingCount.GetValue(inst) as ushort[];
            m_incomingOffers = incomingOffers.GetValue(inst) as TransferManager.TransferOffer[];
            m_incomingAmount = incomingAmount.GetValue(inst) as int[];
            m_outgoingCount = outgoingCount.GetValue(inst) as ushort[];
            m_outgoingOffers = outgoingOffers.GetValue(inst) as TransferManager.TransferOffer[];
            m_outgoingAmount = outgoingAmount.GetValue(inst) as int[];

            InitDelegate();
        }

        public static TransferManager.TransferOffer[] m_outgoingOffers;

        public static TransferManager.TransferOffer[] m_incomingOffers;

        public static ushort[] m_outgoingCount;

        public static ushort[] m_incomingCount;

        public static int[] m_outgoingAmount;

        public static int[] m_incomingAmount;

        public static bool CanUseNewMatchOffers(TransferReason material)
        {
            switch (material)
            {
                case TransferReason.Worker0:
                case TransferReason.Worker1:
                case TransferReason.Worker2:
                case TransferReason.Worker3:
                    return true;
                default: return false;
            }
        }

        public static bool TransferManagerMatchOffersPrefix(TransferReason material)
        {
            if (CanUseNewMatchOffers(material))
            {
                MatchOffers(material);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void MatchOffers(TransferReason material)
        {
            if (!_init)
            {
                Init();
                _init = true;
            }

            if (material != TransferReason.None)
            {
                float distanceMultiplier = TransferManagerGetDistanceMultiplierDG(material);
                float maxDistance = (distanceMultiplier == 0f) ? 0f : (0.01f / distanceMultiplier);
                for (int priority = 7; priority >= 0; priority--)
                {
                    int offerIdex = (int)material * 8 + priority;
                    int incomingCount = m_incomingCount[offerIdex];
                    int outgoingCount = m_outgoingCount[offerIdex];
                    int incomingIdex = 0;
                    int outgoingIdex = 0;
                    while (incomingIdex < incomingCount || outgoingIdex < outgoingCount)
                    {
                        //use incomingOffer to match outgoingOffer
                        if (incomingIdex < incomingCount)
                        {
                            TransferOffer incomingOffer = m_incomingOffers[offerIdex * 256 + incomingIdex];
                            Vector3 incomingPosition = incomingOffer.Position;
                            int incomingOfferAmount = incomingOffer.Amount;
                            // NON-STOCK CODE START
                            TransferReason material2 = material;
                            do
                            {
                                // NON-STOCK CODE END
                                do
                                {
                                    int incomingPriority = Mathf.Max(0, 2 - priority);
                                    int incomingPriorityExclude = (!incomingOffer.Exclude) ? incomingPriority : Mathf.Max(0, 3 - priority);
                                    int validPriority = -1;
                                    int validOutgoingIdex = -1;
                                    float distanceOffsetPre = -1f;
                                    int outgoingIdexInsideIncoming = outgoingIdex;
                                    for (int incomingPriorityInside = priority; incomingPriorityInside >= incomingPriority; incomingPriorityInside--)
                                    {
                                        int outgoingIdexWithPriority = (int)material2 * 8 + incomingPriorityInside;
                                        int outgoingCountWithPriority = m_outgoingCount[outgoingIdexWithPriority];
                                        //To let incomingPriorityInsideFloat!=0
                                        float incomingPriorityInsideFloat = (float)incomingPriorityInside + 0.1f;
                                        //Higher priority will get more chance to match
                                        if (distanceOffsetPre >= incomingPriorityInsideFloat)
                                        {
                                            break;
                                        }
                                        //Find the nearest offer to match in every priority.
                                        for (int i = outgoingIdexInsideIncoming; i < outgoingCountWithPriority; i++)
                                        {
                                            TransferOffer outgoingOfferPre = m_outgoingOffers[outgoingIdexWithPriority * 256 + i];
                                            if (incomingOffer.m_object != outgoingOfferPre.m_object && (!outgoingOfferPre.Exclude || incomingPriorityInside >= incomingPriorityExclude))
                                            {
                                                float incomingOutgoingDistance = Vector3.SqrMagnitude(outgoingOfferPre.Position - incomingPosition);
                                                float distanceOffset = (!(distanceMultiplier < 0f)) ? (incomingPriorityInsideFloat / (1f + incomingOutgoingDistance * distanceMultiplier)) : (incomingPriorityInsideFloat - incomingPriorityInsideFloat / (1f - incomingOutgoingDistance * distanceMultiplier));
                                                if (distanceOffset > distanceOffsetPre)
                                                {
                                                    validPriority = incomingPriorityInside;
                                                    validOutgoingIdex = i;
                                                    distanceOffsetPre = distanceOffset;
                                                    if (incomingOutgoingDistance < maxDistance)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        outgoingIdexInsideIncoming = 0;
                                    }
                                    if (validPriority == -1)
                                    {
                                        break;
                                    }
                                    //Find a validPriority, get outgoingOffer
                                    int matchedOutgoingOfferIdex = (int)material2 * 8 + validPriority;
                                    TransferOffer outgoingOffer = m_outgoingOffers[matchedOutgoingOfferIdex * 256 + validOutgoingIdex];
                                    int outgoingOfferAmount = outgoingOffer.Amount;
                                    int matchedOfferAmount = Mathf.Min(incomingOfferAmount, outgoingOfferAmount);
                                    if (matchedOfferAmount != 0)
                                    {
                                        TransferManagerStartTransferDG(instance, material2, outgoingOffer, incomingOffer, matchedOfferAmount);
                                    }
                                    incomingOfferAmount -= matchedOfferAmount;
                                    outgoingOfferAmount -= matchedOfferAmount;
                                    //matched outgoingOffer is empty now
                                    if (outgoingOfferAmount == 0)
                                    {
                                        int outgoingCountPost = m_outgoingCount[matchedOutgoingOfferIdex] - 1;
                                        m_outgoingCount[matchedOutgoingOfferIdex] = (ushort)outgoingCountPost;
                                        m_outgoingOffers[matchedOutgoingOfferIdex * 256 + validOutgoingIdex] = m_outgoingOffers[matchedOutgoingOfferIdex * 256 + outgoingCountPost];
                                        if (matchedOutgoingOfferIdex == offerIdex)
                                        {
                                            outgoingCount = outgoingCountPost;
                                        }
                                    }
                                    else
                                    {
                                        outgoingOffer.Amount = outgoingOfferAmount;
                                        m_outgoingOffers[matchedOutgoingOfferIdex * 256 + validOutgoingIdex] = outgoingOffer;
                                    }
                                    incomingOffer.Amount = incomingOfferAmount;
                                }
                                while (incomingOfferAmount != 0);
                                // NON-STOCK CODE START
                                if (incomingOfferAmount == 0 || material2 < TransferManager.TransferReason.Worker0 || TransferManager.TransferReason.Worker3 <= material2)
                                {
                                    break;
                                }
                                material2++;
                            } while (true);
                            // NON-STOCK CODE END
                            //matched incomingOffer is empty now
                            if (incomingOfferAmount == 0)
                            {
                                incomingCount--;
                                m_incomingCount[offerIdex] = (ushort)incomingCount;
                                m_incomingOffers[offerIdex * 256 + incomingIdex] = m_incomingOffers[offerIdex * 256 + incomingCount];
                            }
                            else
                            {
                                incomingOffer.Amount = incomingOfferAmount;
                                m_incomingOffers[offerIdex * 256 + incomingIdex] = incomingOffer;
                                incomingIdex++;
                            }
                        }
                        //use outgoingOffer to match incomingOffer
                        if (outgoingIdex < outgoingCount)
                        {
                            TransferOffer outgoingOffer = m_outgoingOffers[offerIdex * 256 + outgoingIdex];
                            Vector3 outgoingOfferPosition = outgoingOffer.Position;
                            int outgoingOfferAmount = outgoingOffer.Amount;
                            // NON-STOCK CODE START
                            TransferReason material2 = material;
                            do
                            {
                                // NON-STOCK CODE END
                                do
                                {
                                    int outgoingPriority = Mathf.Max(0, 2 - priority);
                                    int outgoingPriorityExclude = (!outgoingOffer.Exclude) ? outgoingPriority : Mathf.Max(0, 3 - priority);
                                    int validPriority = -1;
                                    int validIncomingIdex = -1;
                                    float distanceOffsetPre = -1f;
                                    int incomingIdexInsideOutgoing = incomingIdex;
                                    for (int outgoingPriorityInside = priority; outgoingPriorityInside >= outgoingPriority; outgoingPriorityInside--)
                                    {
                                        int incomingIdexWithPriority = (int)material2 * 8 + outgoingPriorityInside;
                                        int incomingCountWithPriority = m_incomingCount[incomingIdexWithPriority];
                                        //To let outgoingPriorityInsideFloat!=0
                                        float outgoingPriorityInsideFloat = (float)outgoingPriorityInside + 0.1f;
                                        //Higher priority will get more chance to match
                                        if (distanceOffsetPre >= outgoingPriorityInsideFloat)
                                        {
                                            break;
                                        }
                                        for (int j = incomingIdexInsideOutgoing; j < incomingCountWithPriority; j++)
                                        {
                                            TransferOffer incomingOfferPre = m_incomingOffers[incomingIdexWithPriority * 256 + j];
                                            if (outgoingOffer.m_object != incomingOfferPre.m_object && (!incomingOfferPre.Exclude || outgoingPriorityInside >= outgoingPriorityExclude))
                                            {
                                                float incomingOutgoingDistance = Vector3.SqrMagnitude(incomingOfferPre.Position - outgoingOfferPosition);
                                                float distanceOffset = (!(distanceMultiplier < 0f)) ? (outgoingPriorityInsideFloat / (1f + incomingOutgoingDistance * distanceMultiplier)) : (outgoingPriorityInsideFloat - outgoingPriorityInsideFloat / (1f - incomingOutgoingDistance * distanceMultiplier));
                                                if (distanceOffset > distanceOffsetPre)
                                                {
                                                    validPriority = outgoingPriorityInside;
                                                    validIncomingIdex = j;
                                                    distanceOffsetPre = distanceOffset;
                                                    if (incomingOutgoingDistance < maxDistance)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        incomingIdexInsideOutgoing = 0;
                                    }
                                    if (validPriority == -1)
                                    {
                                        break;
                                    }
                                    //Find a validPriority, get incomingOffer
                                    int matchedIncomingOfferIdex = (int)material2 * 8 + validPriority;
                                    TransferOffer incomingOffer = m_incomingOffers[matchedIncomingOfferIdex * 256 + validIncomingIdex];
                                    int incomingOfferAmount = incomingOffer.Amount;
                                    int matchedOfferAmount = Mathf.Min(outgoingOfferAmount, incomingOfferAmount);
                                    if (matchedOfferAmount != 0)
                                    {
                                        TransferManagerStartTransferDG(instance, material2, outgoingOffer, incomingOffer, matchedOfferAmount);
                                    }
                                    outgoingOfferAmount -= matchedOfferAmount;
                                    incomingOfferAmount -= matchedOfferAmount;
                                    //matched incomingOffer is empty now
                                    if (incomingOfferAmount == 0)
                                    {
                                        int incomingCountPost = m_incomingCount[matchedIncomingOfferIdex] - 1;
                                        m_incomingCount[matchedIncomingOfferIdex] = (ushort)incomingCountPost;
                                        m_incomingOffers[matchedIncomingOfferIdex * 256 + validIncomingIdex] = m_incomingOffers[matchedIncomingOfferIdex * 256 + incomingCountPost];
                                        if (matchedIncomingOfferIdex == offerIdex)
                                        {
                                            incomingCount = incomingCountPost;
                                        }
                                    }
                                    else
                                    {
                                        incomingOffer.Amount = incomingOfferAmount;
                                        m_incomingOffers[matchedIncomingOfferIdex * 256 + validIncomingIdex] = incomingOffer;
                                    }
                                    outgoingOffer.Amount = outgoingOfferAmount;
                                }
                                while (outgoingOfferAmount != 0);
                                // NON-STOCK CODE START
                                if (outgoingOfferAmount == 0 || material2 < TransferManager.TransferReason.Worker0 || TransferManager.TransferReason.Worker3 <= material2)
                                {
                                    break;
                                }
                                material2++;
                            } while (true);
                            // NON-STOCK CODE END
                            //matched outgoingOffer is empty now
                            if (outgoingOfferAmount == 0)
                            {
                                outgoingCount--;
                                m_outgoingCount[offerIdex] = (ushort)outgoingCount;
                                m_outgoingOffers[offerIdex * 256 + outgoingIdex] = m_outgoingOffers[offerIdex * 256 + outgoingCount];
                            }
                            else
                            {
                                outgoingOffer.Amount = outgoingOfferAmount;
                                m_outgoingOffers[offerIdex * 256 + outgoingIdex] = outgoingOffer;
                                outgoingIdex++;
                            }
                        }
                    }
                }
                for (int k = 0; k < 8; k++)
                {
                    int num40 = (int)material * 8 + k;
                    m_incomingCount[num40] = 0;
                    m_outgoingCount[num40] = 0;
                }
                m_incomingAmount[(int)material] = 0;
                m_outgoingAmount[(int)material] = 0;
            }
        }

        public static void InitDelegate()
        {
            TransferManagerStartTransferDG = FastDelegateFactory.Create<TransferManagerStartTransfer>(typeof(TransferManager), "StartTransfer", instanceMethod: true);
            TransferManagerGetDistanceMultiplierDG = FastDelegateFactory.Create<TransferManagerGetDistanceMultiplier>(typeof(TransferManager), "GetDistanceMultiplier", instanceMethod: false);
        }

        public delegate void TransferManagerStartTransfer(TransferManager TransferManager, TransferReason material, TransferOffer offerOut, TransferOffer offerIn, int delta);
        public static TransferManagerStartTransfer TransferManagerStartTransferDG;

        public delegate float TransferManagerGetDistanceMultiplier(TransferManager.TransferReason material);
        public static TransferManagerGetDistanceMultiplier TransferManagerGetDistanceMultiplierDG;
    }
}