using CramTetrisCalcUI;

namespace CramTetrisCalcUi
{
    public partial class ParameterInputForm : Form
    {
        public ParameterInputForm()
        {
            InitializeComponent();
            TetrisSizeDD.DataSource = TetrisArray.AllArrays;
            TetrisSizeDD.DisplayMember = "Name";
            TetrisSizeDD.SelectedIndex = 0;
            UpdateForm();
        }

        private void UpdateForm()
        {
            TetrisArray tetrisArray = TetrisSizeDD.SelectedItem as TetrisArray;
            int sideLength = tetrisArray.SideLength;

            PrimeConnectorCoordPanel.Enabled = !CenteredPrimeConnectorCB.Checked;
            // Keep values at max if they were at max before the change
            bool xAtMax = PrimeConnectorXUD.Value == PrimeConnectorXUD.Maximum;
            bool yAtMax = PrimeConnectorYUD.Value == PrimeConnectorYUD.Maximum;
            bool zAtMax = PrimeConnectorZUD.Value == PrimeConnectorZUD.Maximum;
            PrimeConnectorXUD.Maximum = sideLength;
            PrimeConnectorYUD.Maximum = LayerCountUD.Value;
            PrimeConnectorZUD.Maximum = sideLength;
            if (xAtMax)
            {
                PrimeConnectorXUD.Value = PrimeConnectorXUD.Maximum;
            }
            if (yAtMax)
            {
                PrimeConnectorYUD.Value = PrimeConnectorYUD.Maximum;
            }
            if (zAtMax)
            {
                PrimeConnectorZUD.Value = PrimeConnectorZUD.Maximum;
            }

            if (CenteredPrimeConnectorCB.Checked)
            {
                // Set coordinates to halfway point, 1-indexed
                int halfLength = (int)Math.Ceiling((decimal)sideLength / 2);
                PrimeConnectorXUD.Value = halfLength;
                PrimeConnectorZUD.Value = halfLength;
                PrimeConnectorYUD.Value = LayerCountUD.Value;
            }
        }

        private void TetrisSizeDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void LayerCountUD_ValueChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void CenteredPrimeConnectorCB_CheckedChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            TetrisArray tetrisArray = TetrisSizeDD.SelectedItem as TetrisArray;
            int layerCount = (int)LayerCountUD.Value;
            // Up/Downs are 1-indexed for display, but coordinates are 0-indexed
            Coordinates primeConnectorCoordinates =
                new((int)PrimeConnectorXUD.Value - 1, (int)PrimeConnectorYUD.Value - 1, (int)PrimeConnectorZUD.Value - 1);
            char columnDelimiter = CommaDecimalSeparatorCB.Checked ? ';' : ',';

            CannonComparer comparer = new(tetrisArray, layerCount, primeConnectorCoordinates, columnDelimiter);
            comparer.CannonTest();
        }
    }
}