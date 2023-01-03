using Microsoft.AspNetCore.Components;
using RPS.Core.Models.Dto;
using RPS.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RPS.Web.Server.Components.Dashboard
{
    public partial class IssueChart : ComponentBase
    {
        public object[] Categories { get; set; }
        public List<object> ItemsOpenByMonth { get; set; }
        public List<object> ItemsClosedByMonth { get; set; }

        [Parameter]
        public PtDashboardFilter Filter { get; set; }

        [Inject]
        public IPtDashboardRepository RpsDashData { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Refresh();
        }

        private void Refresh()
        {
            ItemsOpenByMonth = new List<object>();
            ItemsClosedByMonth= new List<object>();
            
            var filteredIssues = RpsDashData.GetFilteredIssues(Filter);
            filteredIssues.MonthItems.ForEach(i =>
            {
                ItemsOpenByMonth.Add(i.Open.Count);
                ItemsClosedByMonth.Add(i.Closed.Count);
            });

            Categories = filteredIssues.Categories.Select(i => (object)i).ToArray();
        }
    }
}
