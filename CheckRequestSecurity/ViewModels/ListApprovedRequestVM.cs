using CheckRequestSecurity.DB;
using CheckRequestSecurity.Models;
using CheckRequestSecurity.Tools;
using CheckRequestSecurity.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckRequestSecurity.ViewModels
{
    public class ListApprovedRequestVM : BaseVM
    {
        private ObservableCollection<Request> requests;
        private Request selectedRequest;
        private List<TypeRequest> typeRequests;
        private TypeRequest selectedTypeRequest;
        private List<SubDivision> subDivisions;
        private SubDivision selectedSubDivision;
        private DateTime selectedDateVisit;
        private string search;

        public string Search 
        { 
            get => search; 
            set
            {
                search = value;
                DoSearch();
            }
                 
        }

        public ObservableCollection<Request> Requests
        {
            get => requests;
            set
            {
                requests = value;
                SignalChanged();
            }
        }

        public Request SelectedRequest
        {
            get => selectedRequest;
            set
            {
                selectedRequest = value;
                SignalChanged();
            }
        }

        public DateTime SelectedDateVisit
        {
            get => selectedDateVisit;
            set
            {
                selectedDateVisit = value;
                DoSearch();
                SignalChanged();
            }
        }

        public List<TypeRequest> TypeRequests
        {
            get => typeRequests;
            set
            {
                typeRequests = value;
                SignalChanged();
            }
        }

        public TypeRequest SelectedTypeRequest
        {
            get => selectedTypeRequest;
            set
            {
                selectedTypeRequest = value;
                DoSearch();
            }
        }

        public List<SubDivision> SubDivisions
        {
            get => subDivisions;
            set
            {
                subDivisions = value;
                SignalChanged();
            }

        }

        public SubDivision SelectedSubDivision
        {
            get => selectedSubDivision;
            set
            {
                selectedSubDivision = value;
                DoSearch();
            }
        }

        public ListApprovedRequestVM()
        {
            SelectedDateVisit = DateTime.Now;
            Requests = new ObservableCollection<Request>(GetRequests());

            TypeRequests = new List<TypeRequest> { new TypeRequest { Id = 0, Name = "Все" } };
            TypeRequests.AddRange(user50_2Context.GetInstance().TypeRequests);
            SelectedTypeRequest = TypeRequests[0];

            SubDivisions = new List<SubDivision> { new SubDivision { Id = 0, Name = "Все" } };
            SubDivisions.AddRange(user50_2Context.GetInstance().SubDivisions);
            SelectedSubDivision = SubDivisions[0];


        }

        public void OpenModalWindow()
        {
            var window = new ModalWindow(selectedRequest);
            window.ShowDialog();
        }

        private void DoSearch()
        {
            IQueryable<Request> searchRequest = GetRequests();

            if(!string.IsNullOrEmpty(Search))
                searchRequest = searchRequest.Where( s => s.VisitorsRequests.FirstOrDefault(v => v.Visitors.Surname.Contains(Search) || v.Visitors.Name.Contains(Search) || v.Visitors.Patronymic.Contains(Search) || v.Visitors.PassportNumber.Contains(Search)) != null);

            if (SelectedDateVisit != null)
                searchRequest = searchRequest.Where(s => s.DateVisit.Value.Date == SelectedDateVisit.Date && s.DateVisit != null);

            if (SelectedTypeRequest != null)
                searchRequest = searchRequest.Where(s => s.TypeRequestId == SelectedTypeRequest.Id || SelectedTypeRequest.Id == 0);

            if (SelectedSubDivision != null)
                searchRequest = searchRequest.Where(s => s.Worker.SubDivisionId == selectedSubDivision.Id || SelectedSubDivision.Id == 0);
            Requests = new ObservableCollection<Request>(searchRequest);
        }

        private static IQueryable<Request> GetRequests()
        {
            return user50_2Context.GetInstance().Requests.Include(s => s.Worker).ThenInclude(s => s.SubDivision).Include(s => s.VisitorsRequests).ThenInclude(s => s.Visitors).ThenInclude( s => s.VisitorsVisits).ThenInclude( s => s.Visit).Where(s => s.StatusId == 2);
        }
    }
}
