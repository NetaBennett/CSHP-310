using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using VendingMachine.Models;

//***********************************
// Student: Bennett, Neta (netab)
//***********************************

namespace VendingMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Models.VendingMachine _vm = Models.VendingMachine.GetInstance();

        public MainWindow()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            UpdateProductPricing();
            UpdateInventoryLevels();
        }

        //Event Handlers
    
        /// <summary>
        /// Called when user (inserts) makes money selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMoney_Click(object sender, RoutedEventArgs e)
        {
            string denominationTag = ((Button)sender).Tag?.ToString() ?? string.Empty;
            DenominationEnum moneyValue = (DenominationEnum)Enum.Parse(typeof(DenominationEnum), denominationTag);
            InsertMoney(moneyValue);
        }

        /// <summary>
        /// Called when user has made a product selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelection_Click(object sender, RoutedEventArgs e)
        {
            string selectionTag = ((Button)sender).Tag?.ToString() ?? string.Empty;
            ProductEnum selectionProduct = (ProductEnum)Enum.Parse(typeof(ProductEnum), selectionTag);
            DispenseProduct(selectionProduct);
        }
     
        /// <summary>
        /// Called when user decides to not make a product selection and opts instead to get 
        /// their money back, if any has been submitted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReturnChange_Click(object sender, RoutedEventArgs e)
        {
            var returnedChange = _vm.ReturnMoneyWithoutSelection();
            var result = FormatReturnedChangeDisplay(returnedChange);
            UpdateTotalPaid();
            MessageBox.Show(result, "Thank You");
        }

        //Supporting Methods
        private decimal GetProductPrice(ProductEnum productEnum) => _vm.GetProductInventory(productEnum)?.Product.Price ?? 0m;
        private int GetProductIventory(ProductEnum productEnum) => _vm.GetProductInventory(productEnum)?.NumUnits ?? 0;       
        private void UpdateTotalPaid() => labelPaymentTotal.Content = $"$ {_vm.GetTotalPaid()}";
        private void UpdateInventoryLevels()
        {
            labelCocaColaInventory.Content = $"({GetProductIventory(ProductEnum.CocaCola)})";
            labelSpriteInventory.Content = $"({GetProductIventory(ProductEnum.Sprite)})";
            labelMountainDewInventory.Content = $"({GetProductIventory(ProductEnum.MountainDew)})";
        }
        private void UpdateProductPricing()
        {
            var priceCC = GetProductPrice(ProductEnum.CocaCola);
            this.labelCocaColaPrice.Content = $"$ {priceCC}";

            var priceSp = GetProductPrice(ProductEnum.Sprite);
            this.labelSpritPrice.Content = $"$ {priceSp}";

            var priceMd = GetProductPrice(ProductEnum.MountainDew);
            this.labelMountainDewPrice.Content = $"$ {priceMd}";
        }
        private void InsertMoney(DenominationEnum denominationEnum)
        {
            Denomination denomination = DenominationFactory.GetDenomination(denominationEnum);
            _vm.InsertDenomination(denomination);
            UpdateTotalPaid();
        }
        private void DispenseProduct(ProductEnum productEnum)
        {
            Product product = _vm.GetProductInventory(productEnum).Product;
            if (_vm.HasSufficientFunds(product.Price))
            {
                try
                {
                    var result = _vm.DispenseProduct(product);
                    UpdateTotalPaid();
                    UpdateInventoryLevels();
                    MessageBox.Show(FormatPurchaseResultDisplay(result), "Thank You");
                }
                catch(InvalidOperationException opEx)
                {
                    MessageBox.Show(opEx.Message, "Error");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred. Please restart the application.", "Error");
                }
            }
            else
            {
                MessageBox.Show($"Insufficient funds for {product.Name}. Add more funds.", "Sorry");
            }
        }
        private string FormatPurchaseResultDisplay(PurchaseResult result)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Enjoy your {result.PurchasedProduct.Name}!" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("------------------------------" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(FormatReturnedChangeDisplay(result.ReturnChange));
            return sb.ToString();
        }
        private string FormatReturnedChangeDisplay(DenominationBag returnedChange)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Your change is: ${returnedChange.BagTotal}" + Environment.NewLine + Environment.NewLine);
            returnedChange.Coins.ForEach(coin => sb.Append($" > {coin.ToString()}" + Environment.NewLine));
            return sb.ToString();
        }


    }
}
