using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DudeFinder.Hubs
{
    public class LocationHub : Hub
    {
        public void UpdateLocation(string partyid,double lat, double lng)
        {
            string connid = Context.ConnectionId;
            Clients.OthersInGroup(partyid).UpdateMember(connid, lat, lng);
        }

        public async Task JoinParty(string partyid)
        {
            await Groups.Add(Context.ConnectionId, partyid);
            //Clients.OthersInGroup(partyid).PartyJoined(Context.ConnectionId);
        }

        public async Task LeaveParty(string partyid)
        {
            await Groups.Remove(Context.ConnectionId, partyid);
            //Clients.OthersInGroup(partyid).PartyLeaved(Context.ConnectionId);
        }

    }
}