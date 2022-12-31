using Panell.Entities;

namespace Panell.Models
{
    public class ChartModel
    {
        public ChartModel()
        {
            ChartRadarItem = new ChartRadarItem();
            ChartRadarItems = new List<ChartRadarItem>();
            ChartRadarHeader = new ChartRadarHeader();
            ChartRadarHeaders = new List<ChartRadarHeader>();
        }
        public ChartRadarHeader ChartRadarHeader { get; set; }

        public List<ChartRadarHeader> ChartRadarHeaders { get; set; }

        public List<ChartRadarItem> ChartRadarItems { get; set; }

        public ChartRadarItem ChartRadarItem { get; set; }
    }
}
