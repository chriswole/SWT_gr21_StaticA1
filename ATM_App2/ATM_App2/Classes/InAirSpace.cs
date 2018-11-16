using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATM_App2.Classes
{
    
    public class InAirSpace
    {
        private List<Track> _tracksInAirspace;
        private Track _currenTrack { get; set; }
        private ITrackOpticsProvider opticsProvider_ { get; set; }

        public EventHandler<AirspaceTrackArgs> AirspaceUpdated;
        public EventHandler<TrackArgs> EnteredTrack;
        public EventHandler<TrackArgs> LeavingTrack;

        public InAirSpace(ITrackOpticsProvider opticsProvider)
        {
            opticsProvider_ = opticsProvider;
        }

        public void OnTrackCreated(object sender, BaseTrackArgs args)
        {
            //exist on list?
                //yes
                   //in scope?
                        //no - left:
                            // send LeavingTrack Event
                            // airspaceupdated event
                            // delete old track
                        //yes - update
                                // calc speed and course
                                // delete old Track
                                // add new track to list top
                                // send AirspacedUpdated Event - _tracksInAirspace

               // no 
                    //in scope?
                        //yes - new:
                            // add track to list
                            // send LeavingTrack Event
                            // send AirspacedUpdated Event - _tracksInAirspace
                         
                        //no - fuck off
                                // discard Track
        }

    }

    //Logic to Calculate Speed and Course, remember explicit tests

}
