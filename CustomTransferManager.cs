using ColossalFramework;
using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EmployOvereducatedWorkers
{
    public class CustomTransferManager : TransferManager
    {
        private static float GetDistanceMultiplier(TransferReason material)
        {
            switch (material)
            {
                case TransferReason.Garbage:
                    return 5E-07f;
                case TransferReason.Crime:
                    return 1E-05f;
                case TransferReason.Sick:
                    return 1E-06f;
                case TransferReason.Dead:
                    return 1E-05f;
                case TransferReason.Worker0:
                    return 1E-07f;
                case TransferReason.Worker1:
                    return 1E-07f;
                case TransferReason.Worker2:
                    return 1E-07f;
                case TransferReason.Worker3:
                    return 1E-07f;
                case TransferReason.Student1:
                    return 2E-07f;
                case TransferReason.Student2:
                    return 2E-07f;
                case TransferReason.Student3:
                    return 2E-07f;
                case TransferReason.Fire:
                    return 1E-05f;
                case TransferReason.Bus:
                    return 1E-05f;
                case TransferReason.Oil:
                    return 1E-07f;
                case TransferReason.Ore:
                    return 1E-07f;
                case TransferReason.Logs:
                    return 1E-07f;
                case TransferReason.Grain:
                    return 1E-07f;
                case TransferReason.Goods:
                    return 1E-07f;
                case TransferReason.PassengerTrain:
                    return 1E-05f;
                case TransferReason.Coal:
                    return 1E-07f;
                case TransferReason.Family0:
                    return 1E-08f;
                case TransferReason.Family1:
                    return 1E-08f;
                case TransferReason.Family2:
                    return 1E-08f;
                case TransferReason.Family3:
                    return 1E-08f;
                case TransferReason.Single0:
                    return 1E-08f;
                case TransferReason.Single1:
                    return 1E-08f;
                case TransferReason.Single2:
                    return 1E-08f;
                case TransferReason.Single3:
                    return 1E-08f;
                case TransferReason.PartnerYoung:
                    return 1E-08f;
                case TransferReason.PartnerAdult:
                    return 1E-08f;
                case TransferReason.Shopping:
                    return 2E-07f;
                case TransferReason.Petrol:
                    return 1E-07f;
                case TransferReason.Food:
                    return 1E-07f;
                case TransferReason.LeaveCity0:
                    return 1E-08f;
                case TransferReason.LeaveCity1:
                    return 1E-08f;
                case TransferReason.LeaveCity2:
                    return 1E-08f;
                case TransferReason.Entertainment:
                    return 2E-07f;
                case TransferReason.Lumber:
                    return 1E-07f;
                case TransferReason.GarbageMove:
                    return 5E-07f;
                case TransferReason.MetroTrain:
                    return 1E-05f;
                case TransferReason.PassengerPlane:
                    return 1E-05f;
                case TransferReason.PassengerShip:
                    return 1E-05f;
                case TransferReason.DeadMove:
                    return 5E-07f;
                case TransferReason.DummyCar:
                    return -1E-08f;
                case TransferReason.DummyTrain:
                    return -1E-08f;
                case TransferReason.DummyShip:
                    return -1E-08f;
                case TransferReason.DummyPlane:
                    return -1E-08f;
                case TransferReason.Single0B:
                    return 1E-08f;
                case TransferReason.Single1B:
                    return 1E-08f;
                case TransferReason.Single2B:
                    return 1E-08f;
                case TransferReason.Single3B:
                    return 1E-08f;
                case TransferReason.ShoppingB:
                    return 2E-07f;
                case TransferReason.ShoppingC:
                    return 2E-07f;
                case TransferReason.ShoppingD:
                    return 2E-07f;
                case TransferReason.ShoppingE:
                    return 2E-07f;
                case TransferReason.ShoppingF:
                    return 2E-07f;
                case TransferReason.ShoppingG:
                    return 2E-07f;
                case TransferReason.ShoppingH:
                    return 2E-07f;
                case TransferReason.EntertainmentB:
                    return 2E-07f;
                case TransferReason.EntertainmentC:
                    return 2E-07f;
                case TransferReason.EntertainmentD:
                    return 2E-07f;
                case TransferReason.Taxi:
                    return 1E-05f;
                case TransferReason.CriminalMove:
                    return 5E-07f;
                case TransferReason.Tram:
                    return 1E-05f;
                case TransferReason.Snow:
                    return 5E-07f;
                case TransferReason.SnowMove:
                    return 5E-07f;
                case TransferReason.RoadMaintenance:
                    return 5E-07f;
                case TransferReason.SickMove:
                    return 1E-07f;
                case TransferReason.ForestFire:
                    return 1E-05f;
                case TransferReason.Collapsed:
                    return 1E-05f;
                case TransferReason.Collapsed2:
                    return 1E-05f;
                case TransferReason.Fire2:
                    return 1E-05f;
                case TransferReason.Sick2:
                    return 1E-06f;
                case TransferReason.FloodWater:
                    return 5E-07f;
                case TransferReason.EvacuateA:
                    return 1E-05f;
                case TransferReason.EvacuateB:
                    return 1E-05f;
                case TransferReason.EvacuateC:
                    return 1E-05f;
                case TransferReason.EvacuateD:
                    return 1E-05f;
                case TransferReason.EvacuateVipA:
                    return 1E-05f;
                case TransferReason.EvacuateVipB:
                    return 1E-05f;
                case TransferReason.EvacuateVipC:
                    return 1E-05f;
                case TransferReason.EvacuateVipD:
                    return 1E-05f;
                case TransferReason.Ferry:
                    return 1E-05f;
                case TransferReason.CableCar:
                    return 1E-05f;
                case TransferReason.Blimp:
                    return 1E-05f;
                case TransferReason.Monorail:
                    return 1E-05f;
                case TransferReason.TouristBus:
                    return 1E-05f;
                case TransferReason.ParkMaintenance:
                    return 5E-07f;
                case TransferReason.TouristA:
                    return 2E-07f;
                case TransferReason.TouristB:
                    return 2E-07f;
                case TransferReason.TouristC:
                    return 2E-07f;
                case TransferReason.TouristD:
                    return 2E-07f;
                case TransferReason.Mail:
                    return 1E-05f;
                case TransferReason.UnsortedMail:
                    return 5E-07f;
                case TransferReason.SortedMail:
                    return 5E-07f;
                case TransferReason.OutgoingMail:
                    return 5E-07f;
                case TransferReason.IncomingMail:
                    return 5E-07f;
                case TransferReason.AnimalProducts:
                    return 1E-07f;
                case TransferReason.Flours:
                    return 1E-07f;
                case TransferReason.Paper:
                    return 1E-07f;
                case TransferReason.PlanedTimber:
                    return 1E-07f;
                case TransferReason.Petroleum:
                    return 1E-07f;
                case TransferReason.Plastics:
                    return 1E-07f;
                case TransferReason.Glass:
                    return 1E-07f;
                case TransferReason.Metals:
                    return 1E-07f;
                case TransferReason.LuxuryProducts:
                    return 1E-07f;
                default:
                    return 1E-07f;
            }
        }

        private void GetParams()
        {
            var inst = Singleton<TransferManager>.instance;
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
        }

        private void SetParams()
        {
            var inst = Singleton<TransferManager>.instance;
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
            incomingCount.SetValue(inst, m_incomingCount);
            incomingOffers.SetValue(inst, m_incomingOffers);
            incomingAmount.SetValue(inst, m_incomingAmount);
            outgoingCount.SetValue(inst, m_outgoingCount);
            outgoingOffers.SetValue(inst, m_outgoingOffers);
            outgoingAmount.SetValue(inst, m_outgoingAmount);
        }

        private TransferManager.TransferOffer[] m_outgoingOffers;

        private TransferManager.TransferOffer[] m_incomingOffers;

        private ushort[] m_outgoingCount;

        private ushort[] m_incomingCount;

        private int[] m_outgoingAmount;

        private int[] m_incomingAmount;

        private void MatchOffers(TransferReason material)
        {
            GetParams();

            if (material != TransferReason.None)
            {
                float distanceMultiplier = GetDistanceMultiplier(material);
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
                                        StartTransfer(material2, outgoingOffer, incomingOffer, matchedOfferAmount);
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
                                        StartTransfer(material2, outgoingOffer, incomingOffer, matchedOfferAmount);
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

            SetParams();
        }

        private void StartTransfer(TransferManager.TransferReason material, TransferManager.TransferOffer offerOut, TransferManager.TransferOffer offerIn, int delta)
        {
            bool active = offerIn.Active;
            bool active2 = offerOut.Active;
            if (active && offerIn.Vehicle != 0)
            {
                Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                ushort vehicle = offerIn.Vehicle;
                VehicleInfo info = vehicles.m_buffer[(int)vehicle].Info;
                offerOut.Amount = delta;
                info.m_vehicleAI.StartTransfer(vehicle, ref vehicles.m_buffer[(int)vehicle], material, offerOut);
            }
            else if (active2 && offerOut.Vehicle != 0)
            {
                Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
                ushort vehicle2 = offerOut.Vehicle;
                VehicleInfo info2 = vehicles2.m_buffer[(int)vehicle2].Info;
                offerIn.Amount = delta;
                info2.m_vehicleAI.StartTransfer(vehicle2, ref vehicles2.m_buffer[(int)vehicle2], material, offerIn);
            }
            else if (active && offerIn.Citizen != 0u)
            {
                Array32<Citizen> citizens = Singleton<CitizenManager>.instance.m_citizens;
                uint citizen = offerIn.Citizen;
                CitizenInfo citizenInfo = citizens.m_buffer[(int)((UIntPtr)citizen)].GetCitizenInfo(citizen);
                if (citizenInfo != null)
                {
                    offerOut.Amount = delta;
                    citizenInfo.m_citizenAI.StartTransfer(citizen, ref citizens.m_buffer[(int)((UIntPtr)citizen)], material, offerOut);
                }
            }
            else if (active2 && offerOut.Citizen != 0u)
            {
                Array32<Citizen> citizens2 = Singleton<CitizenManager>.instance.m_citizens;
                uint citizen2 = offerOut.Citizen;
                CitizenInfo citizenInfo2 = citizens2.m_buffer[(int)((UIntPtr)citizen2)].GetCitizenInfo(citizen2);
                if (citizenInfo2 != null)
                {
                    offerIn.Amount = delta;
                    // NON-STOCK CODE START
                    // For RealCity
                    // Remove cotenancy, otherwise we can not caculate family money
                    bool flag2 = false;
                    bool flag = false;
                    if (Loader.isRealCityRunning)
                    {
                        flag2 = (material == TransferManager.TransferReason.Single0 || material == TransferManager.TransferReason.Single1 || material == TransferManager.TransferReason.Single2 || material == TransferManager.TransferReason.Single3 || material == TransferManager.TransferReason.Single0B || material == TransferManager.TransferReason.Single1B || material == TransferManager.TransferReason.Single2B || material == TransferManager.TransferReason.Single3B);
                        flag = (citizenInfo2.m_citizenAI is ResidentAI) && (Singleton<BuildingManager>.instance.m_buildings.m_buffer[offerIn.Building].Info.m_class.m_service == ItemClass.Service.Residential);
                    }

                    if (flag && flag2)
                    {
                        if (material == TransferManager.TransferReason.Single0 || material == TransferManager.TransferReason.Single0B)
                        {
                            material = TransferManager.TransferReason.Family0;
                        }
                        else if (material == TransferManager.TransferReason.Single1 || material == TransferManager.TransferReason.Single1B)
                        {
                            material = TransferManager.TransferReason.Family1;
                        }
                        else if (material == TransferManager.TransferReason.Single2 || material == TransferManager.TransferReason.Single2B)
                        {
                            material = TransferManager.TransferReason.Family2;
                        }
                        else if (material == TransferManager.TransferReason.Single3 || material == TransferManager.TransferReason.Single3B)
                        {
                            material = TransferManager.TransferReason.Family3;
                        }
                        citizenInfo2.m_citizenAI.StartTransfer(citizen2, ref citizens2.m_buffer[(int)((UIntPtr)citizen2)], material, offerIn);
                    }
                    else
                    {
                        /// NON-STOCK CODE END ///
                        citizenInfo2.m_citizenAI.StartTransfer(citizen2, ref citizens2.m_buffer[(int)((UIntPtr)citizen2)], material, offerIn);
                    }
                }
            }
            else if (active2 && offerOut.Building != 0)
            {
                Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
                ushort building = offerOut.Building;
                ushort building1 = offerIn.Building;
                BuildingInfo info3 = buildings.m_buffer[(int)building].Info;
                offerIn.Amount = delta;
                // NON-STOCK CODE START
                // New Outside Interaction Mod
                if ((material == TransferManager.TransferReason.DeadMove || material == TransferManager.TransferReason.GarbageMove) && Singleton<BuildingManager>.instance.m_buildings.m_buffer[offerOut.Building].m_flags.IsFlagSet(Building.Flags.Untouchable))
                {
                    StartMoreTransfer(building, ref buildings.m_buffer[(int)building], material, offerIn);
                }
                else
                {
                    // NON-STOCK CODE END
                    info3.m_buildingAI.StartTransfer(building, ref buildings.m_buffer[(int)building], material, offerIn);
                }
            }
            else if (active && offerIn.Building != 0)
            {
                Array16<Building> buildings2 = Singleton<BuildingManager>.instance.m_buildings;
                ushort building2 = offerIn.Building;
                BuildingInfo info4 = buildings2.m_buffer[(int)building2].Info;
                offerOut.Amount = delta;
                info4.m_buildingAI.StartTransfer(building2, ref buildings2.m_buffer[(int)building2], material, offerOut);
            }
        }

        public void StartMoreTransfer(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (material == TransferManager.TransferReason.GarbageMove)
            {
                VehicleInfo randomVehicleInfo2 = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level1);
                if (randomVehicleInfo2 != null)
                {
                    Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num2;
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num2, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo2, data.m_position, material, false, true))
                    {
                        randomVehicleInfo2.m_vehicleAI.SetSource(num2, ref vehicles2.m_buffer[(int)num2], buildingID);
                        randomVehicleInfo2.m_vehicleAI.StartTransfer(num2, ref vehicles2.m_buffer[(int)num2], material, offer);
                        vehicles2.m_buffer[num2].m_flags |= (Vehicle.Flags.Importing);
                    }
                }
            }
            else if (material == TransferManager.TransferReason.DeadMove)
            {
                VehicleInfo randomVehicleInfo2 = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level2);
                if (randomVehicleInfo2 != null)
                {
                    Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num2;
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num2, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo2, data.m_position, material, false, true))
                    {
                        randomVehicleInfo2.m_vehicleAI.SetSource(num2, ref vehicles2.m_buffer[(int)num2], buildingID);
                        randomVehicleInfo2.m_vehicleAI.StartTransfer(num2, ref vehicles2.m_buffer[(int)num2], material, offer);
                        vehicles2.m_buffer[num2].m_flags |= (Vehicle.Flags.Importing);
                    }
                }
            }
        }
    }
}