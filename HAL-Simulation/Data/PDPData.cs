namespace HAL_Simulator.Data
{
    public class PDPData : DataBase
    {
        public override void ResetData()
        {
            for (int i = 0; i < Current.Length; i++)
            {
                Current[i] = 0.0;
            }
            HasSource = false;
            Temperature = 0.0;
            Voltage = 0.0;
            TotalEnergy = 0.0;
        }

        internal PDPData()
        {
            for (int i = 0; i < Current.Length; i++)
            {
                Current[i] = 0.0;
            }
        }

        public bool HasSource { get; set; } = false;
        public double Temperature { get; set; } = 0.0;
        public double Voltage { get; set; } = 0.0;
        public double[] Current { get; } = new double[16];
        public double TotalEnergy { get; set; } = 0.0;
    }
}
