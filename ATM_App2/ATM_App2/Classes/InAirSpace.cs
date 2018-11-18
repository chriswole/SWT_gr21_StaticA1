using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_App2.Interfaces;


namespace ATM_App2.Classes
{
    
    public class InAirSpace : IInAirSpace
    {
        //event handlers
        public EventHandler<AirspaceTrackArgs> AirspaceUpdated;
        public EventHandler<TrackArgs> EnteredTrack;
        public EventHandler<TrackArgs> LeavingTrack;

        public InAirSpace(ITrackOpticsProvider opticsProvider)
        {
            opticsProvider_ = opticsProvider;
            TracksInAirspace_ = new List<Track>();
        }

        public void OnTrackCreated(object sender, TrackArgs args)
        {
            ReceivedTrack_ = args.newTrack_;
            int oldIndexOfReceivedTrack;

            //Is Received Track Within Airspace and is it already tracked:
            bool ReceivedTrackIsInAirSpace = InScope(ReceivedTrack_);
            bool ReceivedTrackIdentifiedInList = InList(ReceivedTrack_, out oldIndexOfReceivedTrack);

            //what event to send:
            if (ReceivedTrackIdentifiedInList)
            {
                if (ReceivedTrackIsInAirSpace) //update Track
                {
                    UpdateAirSpaceEventhandler(ref ReceivedTrack_, oldIndexOfReceivedTrack);

                }
                else  // Track Left
                {
                    LeavingTrackEventHandler(ReceivedTrack_, oldIndexOfReceivedTrack);
                }

            }
            else
            {
                if (ReceivedTrackIsInAirSpace) // new Track
                {
                    EnterinTrackEventHandler(ReceivedTrack_);
                }

                //else discard ReceivedTrack_

            }

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
            // send EnteringTrack Event
            // send AirspacedUpdated Event - _tracksInAirspace

            //no - fuck off
            // discard Track
        }


        //Air space definition:  -------------------------------
        private Position southWestCorner = new Position(10000, 10000), northEastCorner = new Position(90000, 90000);
        private int lowest_altitude = 500, highest_altitude = 20000;

        private List<Track> TracksInAirspace_ { get; set; }
        
        //----------------------------------------
        
        private Track ReceivedTrack_;

        private ITrackOpticsProvider opticsProvider_;

        void EnterinTrackEventHandler(Track EnteringTrack)
        {
            TracksInAirspace_.Insert(0, EnteringTrack);
            OnEnteredTrack(EnteringTrack);
            OnAirspaceUpdated(TracksInAirspace_);
        }

        void LeavingTrackEventHandler(Track LeavingTrack, int oldIndex)
        {
            TracksInAirspace_.RemoveAt(oldIndex);
            OnAirspaceUpdated(TracksInAirspace_);
            OnLeavingTrack(LeavingTrack);
        }

        void UpdateAirSpaceEventhandler(ref Track trackToUpdate, int oldIndex)
        {
            UpdateTrack(ref ReceivedTrack_, TracksInAirspace_[oldIndex]);
            TracksInAirspace_.RemoveAt(oldIndex);
            TracksInAirspace_.Insert(0, ReceivedTrack_);
            OnAirspaceUpdated(TracksInAirspace_);
        }

        protected virtual void OnAirspaceUpdated(List<Track> UpdatedAirspaceList)
        {
            AirspaceUpdated?.Invoke(this, new AirspaceTrackArgs(TracksInAirspace_));
        }

        protected virtual void OnEnteredTrack(Track TrackThatEntered)
        {
            EnteredTrack?.Invoke(this, new TrackArgs(TrackThatEntered));
        }

        protected virtual void OnLeavingTrack(Track TrackThatLeft)
        {
            LeavingTrack?.Invoke(this, new TrackArgs(){newTrack_ = TrackThatLeft});
        }

        private bool InScope(Track track)
        {
            return track.pos_ >= (Position)southWestCorner && track.pos_ <= (Position)northEastCorner &&
                   track.altitude_ >= lowest_altitude && track.altitude_ <= highest_altitude;

        }

        private bool InList(Track trackToFind, out int identifiedIndex)
        {
            bool found = false;
            identifiedIndex = -1;

            for (int i = 0; i < TracksInAirspace_.Count; i++)
            {
                if (TracksInAirspace_[i].tag_ == trackToFind.tag_)
                {
                    found = true;
                    identifiedIndex = i;
                }
            }

            return found;

        }

        private void UpdateTrack( ref Track activeTrack, Track passTrack)
        {
            activeTrack.course_ = (int) opticsProvider_.GetTrackCourse(activeTrack, passTrack);
            activeTrack.hori_velocity_ = (int) opticsProvider_.GetTrackVelocity(activeTrack, passTrack);
        }
    }

    //Logic to Calculate Speed and Course, remember explicit tests

}
