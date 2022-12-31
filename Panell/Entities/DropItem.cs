namespace Panell.Entities
{
    public class DropItem
    {
        public DropItem()
        {
            DateTime = new DateTime();
            DateTime2 = new DateTime();
            ItemValues = new List<ItemValue>();
            IntList = new List<int>();
        }

        public string Text { get; set; }
        public string Tanim { get; set; }
        public float IdFloat { get; set; }
        public string DigerTanim { get; set; }
        public string Id { get; set; }
        public string Id2 { get; set; }
        public decimal TanimDecimal { get; set; }
        public int IdInt { get; set; }
        public int IdInt2 { get; set; }
        public int IdInt3 { get; set; }
        public int ParentId { get; set; }
        public int VeriTipi { get; set; }

        public int BagliOlduguId { get; set; }
        public string Type { get; set; }
        public bool SeciliMi { get; set; }
        public bool ZorunluMU { get; set; }
        public string TextDeger { get; set; }

        public string Tanim1 { get; set; }
        public string Tanim2 { get; set; }
        public string Tanim3 { get; set; }
        public string Tanim4 { get; set; }
        public string Tanim5 { get; set; }
        public string Tanim6 { get; set; }
        public string Tanim7 { get; set; }
        public string Tanim8 { get; set; }

        public string Tanim9 { get; set; }
        public string Tanim10 { get; set; }
        public string Tanim11 { get; set; }
        public string Tanim12 { get; set; }
        public string Tanim13 { get; set; }

        public string OzelDeger1 { get; set; }
        public string OzelDeger2 { get; set; }

        public byte[] imageByte { get; set; }

        public DateTime DateTime { get; set; }
        public DateTime DateTime2 { get; set; }
        public List<ItemValue> ItemValues { get; set; }

        public decimal Decimal1 { get; set; }
        public decimal Decimal2 { get; set; }
        public decimal Decimal3 { get; set; }
        public decimal Decimal4 { get; set; }
        public decimal Decimal5 { get; set; }
        public decimal Decimal6 { get; set; }

        public List<int> IntList { get; set; }
    }

    public class ItemValue
    {
        public string Text { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }

        public int IdInt { get; set; }
        public int IdInt2 { get; set; }
        public string Id { get; set; }
        public bool SeciliMi { get; set; }

        public decimal DecimalDeger { get; set; }
        public decimal DecimalDeger2 { get; set; }
    }
}
