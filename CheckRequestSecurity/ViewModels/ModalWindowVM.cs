using CheckRequestSecurity.DB;
using CheckRequestSecurity.Models;
using CheckRequestSecurity.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckRequestSecurity.ViewModels
{
    public class ModalWindowVM : BaseVM
    {
        private int dateVisitHours;
        private int dateVisitMinutes;
        private List<Status> status;
        private bool block = true;
        private DateTime selectedDate;
        private int dateEndHour;
        private int dateEndMinutes;
        private Visit visit;
        private bool block2;

        public Command Welcome { get; set; }
        public Command NotWelcome { get; set; }

        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                SignalChanged();
            }
        }

        public int DateEndHours
        {
            get => dateEndHour;
            set
            {
                dateEndHour = value;
                SignalChanged();
            }
        }

        public int DateEndMinutes 
        { 
            get => dateEndMinutes;
            set
            {
                dateEndMinutes = value;
                SignalChanged();
            }
        }

        public bool Block
        {
            get => block;
            set
            {
                block = value;
                SignalChanged();
            }
        }
        public bool Block2
        {
            get => block2;
            set
            {
                block2 = value;
                SignalChanged();
            }
        }

        public int DateVisitHours
        {
            get => dateVisitHours;
            set
            {
                dateVisitHours = value;
                SignalChanged();
            }

        }

        public int DateVisitMinutes
        {
            get => dateVisitMinutes;
            set
            {
                dateVisitMinutes = value;
                SignalChanged();
            }
        }

        public List<Status> Status
        {
            get => status;
            set
            {
                status = value;
                SignalChanged();
            }

        }

        public Request Request { get; set; }
        public ModalWindowVM(Models.Request selectedRequest)
        {
            SelectedDate = DateTime.Now;
            Request = selectedRequest;
            DateVisitHours = selectedRequest.DateVisit.Value.Hour;
            DateVisitMinutes = selectedRequest.DateVisit.Value.Minute;
            Status = user50_2Context.GetInstance().Statuses.ToList();
            VisitorsVisit visitorsVisit = selectedRequest.VisitorsRequests.First().Visitors.VisitorsVisits.First();
            if (visitorsVisit != null)
            {
                if(visitorsVisit.Visit.DateEnd != null)
                {
                    Block2 = false;
                    SelectedDate = visitorsVisit.Visit.DateEnd.Value;
                    DateEndHours = visitorsVisit.Visit.DateEnd.Value.Hour;
                    DateEndMinutes = visitorsVisit.Visit.DateEnd.Value.Minute;
                }
                Block = false;
                visit = visitorsVisit.Visit;
            }
            else
            {
                Block2 = false;
            }
            Welcome = new Command(() =>
            {
                if (DateTime.Now < selectedRequest.DateVisit)
                {
                    MessageBox.Show("РАНО!");
                    return;
                }
                else
                {
                    System.Media.SystemSounds.Hand.Play();
                    Visit visit = new Visit { DateStart = DateTime.Now, WorkerId = selectedRequest.WorkerId };
                    //user50_2Context.GetInstance().Visits.Add(visit);
                    //user50_2Context.GetInstance().SaveChanges();
                    
                    foreach (VisitorsRequest visitor in selectedRequest.VisitorsRequests)
                    {
                        VisitorsVisit visitorVisit = new VisitorsVisit { VisitorsId = visitor.VisitorsId, Visit = visit };
                        user50_2Context.GetInstance().Add(visitorVisit);

                    }
                    user50_2Context.GetInstance().SaveChanges();
                    this.visit = user50_2Context.GetInstance().Visits.ToList().Last();
                    Block = false;
                    Block2 = true;
                }
            });
            
            NotWelcome = new Command(() =>
            {
                visit.DateEnd = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, DateEndHours, DateEndMinutes, 0);
                if (visit.DateEnd < visit.DateStart )
                {
                    MessageBox.Show("Пользователи не могут выйти раньше чем зашли");
                    return;
                }
                else
                {
                    
                    user50_2Context.GetInstance().Update(visit);
                    user50_2Context.GetInstance().SaveChanges();
                    Block2 = false;
                }
            });
        }
    }
}
