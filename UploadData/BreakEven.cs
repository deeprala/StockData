using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadData
{
    public partial class BreakEven : Form
    {
        public double amountSpent = 0.0;
        public double currentAmount = 0.0;
        public BreakEven()
        {
            InitializeComponent();
        }

        private void txtOldPrice_TextChanged(object sender, EventArgs e)
        {

            amountSpent = Convert.ToDouble(txtOldStockNumbers.Text) * Convert.ToDouble(txtOldPrice.Text);
            txtAmountSpent.Text = amountSpent.ToString();
        }

        private void txtCurrentPrice_TextChanged(object sender, EventArgs e)
        {
            double longPresentUpDownAmt = 0.0;
            double longPresentUpDownStock = 0.0;

            currentAmount = Convert.ToDouble(txtOldStockNumbers.Text) * Convert.ToDouble(txtCurrentPrice.Text);
            txtCurrentAmount.Text = currentAmount.ToString("F");

            longPresentUpDownAmt = -(1 - currentAmount / amountSpent) * 100;
            txtLngUpDownAmt.Text = longPresentUpDownAmt.ToString("F");

            longPresentUpDownStock = -(1 - Convert.ToDouble(txtCurrentPrice.Text) / Convert.ToDouble(txtOldPrice.Text)) * 100;
            txtLngUpDownStock.Text = longPresentUpDownStock.ToString("F");

            txtShortUpDownAmount.Text  = (-longPresentUpDownAmt).ToString("F");
            txtShortUpDownStock.Text  = (-longPresentUpDownStock).ToString("F");

            txtLongProfitLoss.Text = (currentAmount - amountSpent).ToString("F");


            txtShortProfitLoss.Text = (-(currentAmount - amountSpent)).ToString("F");

        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            Calculate();
           
        }

        private void txtBreakEvenMoney_TextChanged(object sender, EventArgs e)
        {
            

        }

        public void Calculate()
        {
            txtUnroundedStockstoBuy.Text =
                (Convert.ToDouble(txtBreakEvenMoney.Text) / Convert.ToDouble(txtCurrentPrice.Text)).ToString("F");

            txtRoundedStockstoBuy.Text = Math.Round(Convert.ToDecimal(txtUnroundedStockstoBuy.Text)).ToString();

            txtAdjBreakEvenMoney.Text =
                (Convert.ToDouble(txtCurrentPrice.Text) * Convert.ToInt64(txtRoundedStockstoBuy.Text)).ToString("F");

            txtTotalShares.Text =
                (Convert.ToDecimal(txtOldStockNumbers.Text) + Convert.ToDecimal(txtUnroundedStockstoBuy.Text)).ToString("####");

            txtTotalAmount.Text = (amountSpent + Convert.ToDouble(txtAdjBreakEvenMoney.Text)).ToString("F");

            txtAvgPrice.Text = (Convert.ToDouble(txtTotalAmount.Text) / Convert.ToDouble(txtTotalShares.Text)).ToString("F");

            txtLongAvgPercent.Text = (-(1 - Convert.ToDouble(txtCurrentPrice.Text) / Convert.ToDouble(txtAvgPrice.Text)) *
                                      100).ToString("F") ;
            //txtLongAvgPercent.Text = txtLongAvgPercent.Text.Contains("-") ? txtLongAvgPercent.Font. ;
            txtLngAvgReductionPercent.Text =
                (-(-Convert.ToDouble(txtLngUpDownStock.Text) - -Convert.ToDouble(txtLongAvgPercent.Text))).ToString("F") ;

            txtShortAvgPercent.Text = ((1 - Convert.ToDouble(txtCurrentPrice.Text) / Convert.ToDouble(txtAvgPrice.Text)) *
                                       100).ToString("F") ;

            txtShortAvgReductionPercent.Text =
                (-Convert.ToDouble(txtLngUpDownStock.Text) - -Convert.ToDouble(txtLongAvgPercent.Text)).ToString("F") ;

            txtLongAvgProfitLoss.Text =
                ((currentAmount + Convert.ToDouble(txtBreakEvenMoney.Text) - Convert.ToDouble(txtTotalAmount.Text)))
                .ToString("F");

            txtShortAvgProfitLoss.Text =
                (-(currentAmount + Convert.ToDouble(txtBreakEvenMoney.Text) - Convert.ToDouble(txtTotalAmount.Text)))
                .ToString("F");
        }
    }
}
