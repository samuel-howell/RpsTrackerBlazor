using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration.UserSecrets;
using RPS.Core.Models;
using RPS.Core.Models.Dto;
using RPS.Data;
using RPS.Web.Server.Models;

namespace RPS.Web.Server.Pages
{
    public partial class Dashboard : ComponentBase
    {
        [Inject]
        IPtDashboardRepository RpsDashRepo { get; set; }



        [Parameter]
        public int? Months { get; set; }

        [Parameter]
        public int? UserId { get; set; }


        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public int IssueCountOpen { get; set; }
        public int IssueCountClosed { get; set; }

        public int IssueCountActive { get { return IssueCountOpen + IssueCountClosed; } }
        public decimal IssueCloseRate { get { if (IssueCountActive == 0) return 0;  return Math.Round((decimal)IssueCountClosed / (decimal)IssueCountActive * 100m, 2); } }

        public PtDashboardFilter Filter { get; set; }


        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Refresh();
        }

        protected override void OnInitialized()
        {
            Assignees = RpsUserRepo.GetAll().ToList();

            GridData = new List<MachineData>();

            var rnd = new Random();

            for (int i = 1; i <= 30; i++)
            {
                GridData.Add(new MachineData
                {
                    id = i,
                    coilNumber = rnd.Next(800000),
                    dateRan = RandomDay(),
                    start = RandomTime(),
                    stop = RandomTime(),
                    crew = getCrew(),
                    duration = RandomTime(),
                    grade = rnd.Next(50),
                    product = rnd.Next(50),
                    passes = rnd.Next(10),
                    gauge = rnd.NextDouble(),
                    nominal = rnd.NextDouble(),
                    reduction = rnd.NextDouble(),
                    width = rnd.NextDouble(),
                    speed = rnd.NextDouble(),
                    feet = rnd.NextDouble() * 6,
                    heatNo = rnd.Next(50),
                    bonus = rnd.NextDouble() * 4,
                    time = rnd.NextDouble() * 13



                });
            }
        }

        private void SingleSelectionChangeHandler(DateTime newValue)
        {
            DateStart = newValue;
            DateEnd = DateTime.Now;
            Refresh();
        }

        private void Refresh()
        {
            DateTime start = Months.HasValue ? DateTime.Now.AddMonths(Months.Value * -1) : DateTime.Now.AddYears(-5);
            //DateTime start = DateStart.HasValue ? DateStart.Value : DateTime.Now.AddYears(-5);
            DateTime end = DateTime.Now;

            Filter = new PtDashboardFilter
            {
                DateStart = start,
                DateEnd = end,
                UserId = UserId.HasValue ? UserId.Value : 0
            };

            
            var statusCounts = RpsDashRepo.GetStatusCounts(Filter);
            IssueCountOpen = statusCounts.OpenItemsCount;
            IssueCountClosed = statusCounts.ClosedItemsCount;

            if (Months.HasValue)
            {
                DateStart = Filter.DateStart;
                DateEnd = Filter.DateEnd;
            }
        }

        [Inject]
        private IPtUserRepository RpsUserRepo { get; set; }

        public int? SelectedAssigneeId
        {
            get { return UserId; }
            set
            {
                UserId = value.HasValue ? value : 0;
                Months = Months.HasValue ? Months : 12;
                NavigationManager.NavigateTo($"/dashboard/{Months}/{UserId}");
            }
        }

        public List<PtUser> Assignees { get; set; }
    }
}