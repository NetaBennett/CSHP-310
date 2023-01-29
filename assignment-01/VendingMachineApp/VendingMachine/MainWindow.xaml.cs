using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private decimal GetProductPrice(ProductEnum productEnum) => 
            _vm.GetProductInventory(productEnum)?.Product.Price ?? 0m;

        private int GetProductIventory(ProductEnum productEnum) => 
                _vm.GetProductInventory(productEnum)?.NumUnits ?? 0;
        private void buttonCocaCola_Click(object sender, RoutedEventArgs e) => DispenseProduct(ProductFactory.CocaCola);
        private void buttonSprite_Click(object sender, RoutedEventArgs e) => DispenseProduct(ProductFactory.Sprite);
        private void buttonMountainDew_Click(object sender, RoutedEventArgs e) => DispenseProduct(ProductFactory.MountainDew);
        private void buttonNickel_Click(object sender, RoutedEventArgs e) => InsertMoney(DenominationFactory.Nickel);
        private void buttonDime_Click(object sender, RoutedEventArgs e) => InsertMoney(DenominationFactory.Dime);
        private void buttonQuarter_Click(object sender, RoutedEventArgs e) => InsertMoney(DenominationFactory.Quarter);
        private void buttonHalfDollar_Click(object sender, RoutedEventArgs e) => InsertMoney(DenominationFactory.HalfDollar);
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
        private void InsertMoney(Denomination denomination)
        {
            _vm.InsertDenomination(denomination);
            UpdateTotalPaid();
        }

        private void DispenseProduct(Product product)
        {
            if (_vm.HasSufficientFunds(product))
            {
                try
                {
                    var result = _vm.DispenseProduct(product);
                    UpdateTotalPaid();
                    UpdateInventoryLevels();
                    MessageBox.Show(FormatResultDisplay(result), "Purchase Complete");
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
                MessageBox.Show("Insufficient funds for product. Add more funds.", "Sorry");
            }
        }

        private string FormatResultDisplay(PurchaseResult result)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Enjoy your {result.PurchasedProduct.Name}!" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("------------------------------" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"Your change is: ${result.ReturnChange.BagTotal}" + Environment.NewLine);
            result.ReturnChange.Coins.ForEach(coin => sb.Append($" > {coin.ToString()}" + Environment.NewLine));

            return sb.ToString();
        }
    }
}
