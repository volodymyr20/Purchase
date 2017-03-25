using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Purchase
{
    public class Discount
    {
        public String name { get; set; }
        public Int16 percent { get; set; }

    }
    public class SpecialPrice
    {
        public Int16 quantity { get; set; }
        public float price { get; set; }
    }
    public class PriceListItem
    {
        public String name { get; set; }
        public List<SpecialPrice> _SpecialPrice { get; set; }
        public List<Discount> _Discount { get; set; }

        public PriceListItem()
        {
            _SpecialPrice = new List<SpecialPrice>();
            _Discount = new List<Discount>();
        }
    }

    public class PurchaseItem
    {
        public String name { get; set; }
        public Int16 quantity { get; set; }
    }

    public class PriceList
    {
        public List<PriceListItem> _Content { get; set; }
        public PriceList()
        {
            _Content = new List<PriceListItem>();

            // Milk
            PriceListItem _PriceListItem = new PriceListItem();
            SpecialPrice _SpecialPrice = new SpecialPrice();
            _PriceListItem.name = "Milk";
            _SpecialPrice.quantity = 1;
            _SpecialPrice.price = 10;
            _PriceListItem._SpecialPrice.Add(_SpecialPrice);

            _SpecialPrice = new SpecialPrice();
            _SpecialPrice.quantity = 5; // here and below: assuming quantity is ascending
            _SpecialPrice.price = 8;
            _PriceListItem._SpecialPrice.Add(_SpecialPrice);

            _SpecialPrice = new SpecialPrice();
            _SpecialPrice.quantity = 10;
            _SpecialPrice.price = 6;
            _PriceListItem._SpecialPrice.Add(_SpecialPrice);

            _Content.Add(_PriceListItem);

            // Water
            _PriceListItem = new PriceListItem();
            _SpecialPrice = new SpecialPrice();
            _PriceListItem.name = "Water";
            _SpecialPrice.quantity = 1;
            _SpecialPrice.price = 6;
            _PriceListItem._SpecialPrice.Add(_SpecialPrice);

            _SpecialPrice = new SpecialPrice();
            _SpecialPrice.quantity = 6;
            _SpecialPrice.price = 5;
            _PriceListItem._SpecialPrice.Add(_SpecialPrice);

            _SpecialPrice = new SpecialPrice();
            _SpecialPrice.quantity = 8;
            _SpecialPrice.price = 4;
            _PriceListItem._SpecialPrice.Add(_SpecialPrice);

            _SpecialPrice = new SpecialPrice();
            Discount _Discount = new Discount();
            _Discount.name = "Sugar";
            _Discount.percent = 10; // assuming quantity is descending
            _PriceListItem._Discount.Add(_Discount);

            _Content.Add(_PriceListItem);

            // Sugar
            _PriceListItem = new PriceListItem();
            _SpecialPrice = new SpecialPrice();
            _Discount = new Discount();
            _PriceListItem.name = "Sugar";
            _SpecialPrice.quantity = 1;
            _SpecialPrice.price = 15;

            _PriceListItem._SpecialPrice.Add(_SpecialPrice);

            _SpecialPrice = new SpecialPrice();

            _SpecialPrice.quantity = 10;
            _SpecialPrice.price = 11;

            _PriceListItem._SpecialPrice.Add(_SpecialPrice);
            _PriceListItem._Discount.Add(_Discount);

            _SpecialPrice = new SpecialPrice();
            _SpecialPrice.quantity = 15;
            _SpecialPrice.price = 9;
            _PriceListItem._SpecialPrice.Add(_SpecialPrice);

            _Content.Add(_PriceListItem);

        }
    }

    public class ReceiptItem
    {
        public String name { get; set; }
        public Int16 quantity { get; set; }
        public float price { get; set; }
    }
    public class Receipt
    {
        public List<ReceiptItem> _Content { get; set; }
        public float total { get; set; }
        public Receipt()
        {
            _Content = new List<ReceiptItem>();
            total = 0;
        }
        public Int16 AddItemToReceipt(PriceList _PriceList, String itemName)
        {
            ReceiptItem _ReceiptItem = new ReceiptItem();

            IEnumerable<PriceListItem> tieredPrice = _PriceList._Content.Where(obj => ((obj.name == itemName)));

            for (int i = 0; i < tieredPrice.First()._SpecialPrice.Count; i++)
            {
                if (tieredPrice.First()._SpecialPrice[i].quantity == 1)
                {
                    _ReceiptItem.price = tieredPrice.First()._SpecialPrice[i].price;
                    break;
                }
            }
            _ReceiptItem.name = itemName;
            _ReceiptItem.quantity = 1;

            _Content.Add(_ReceiptItem);

            total += _ReceiptItem.price;

            return 0;
        }
        public Int16 AddItemToReceipt(PriceList _PriceList, String itemName, Int16 qty)
        {
            IEnumerable<PriceListItem> tieredPrice = _PriceList._Content.Where(obj => (obj.name == itemName));

            Int16 initialQty = qty;
            float subtotal = 0;

            do
            {
                for (int j = tieredPrice.First()._SpecialPrice.Count - 1; j >= 0; j--)
                {
                    if (qty >= tieredPrice.First()._SpecialPrice[j].quantity)
                    {
                        subtotal += tieredPrice.First()._SpecialPrice[j].price * tieredPrice.First()._SpecialPrice[j].quantity;
                        total += tieredPrice.First()._SpecialPrice[j].price * tieredPrice.First()._SpecialPrice[j].quantity;
                        qty -= tieredPrice.First()._SpecialPrice[j].quantity;
                    }
                }

            } while (qty == 1);

            ReceiptItem _ReceiptItem = new ReceiptItem();

            _ReceiptItem.name = itemName;
            _ReceiptItem.quantity = initialQty;
            _ReceiptItem.price = subtotal;
            _Content.Add(_ReceiptItem);

            return 0;

        }
        public class Bucket
        {
            public List<PurchaseItem> _Content { get; set; }
            public Bucket()
            {
                _Content = new List<PurchaseItem>();
            }
            public Int16 AddItemToBucket(String itemName)
            {
                PurchaseItem _PurchaseItem = new PurchaseItem();
                _PurchaseItem.name = itemName;
                _PurchaseItem.quantity = 1;
                _Content.Add(_PurchaseItem);
                return 0;
            }
            public Int16 AddItemToBucket(String itemName, Int16 qty)
            {
                PurchaseItem _PurchaseItem = new PurchaseItem();
                _PurchaseItem.name = itemName;
                _PurchaseItem.quantity = qty;
                _Content.Add(_PurchaseItem);
                return 0;
            }

            public Int16 RemoveItemFromBucket() // TBD
            {
                return 0;
            }
        }

        public class Client
        {
            public Bucket _Bucket { get; set; }
            public Receipt _Receipt { get; set; }

            public Client()
            {
                _Bucket = new Bucket();
                _Receipt = new Receipt();
            }
        }

        public class Casier
        {
            public Client _Client { get; set; }

            public Int16 ScanItem(PriceList _PriceList, String itemName)
            {
                _Client._Bucket.AddItemToBucket(itemName);
                _Client._Receipt.AddItemToReceipt(_PriceList, itemName);
                return 0;
            }

            public Int16 ScanItem(PriceList _PriceList, String itemName, Int16 qty)
            {
                _Client._Bucket.AddItemToBucket(itemName, qty);
                _Client._Receipt.AddItemToReceipt(_PriceList, itemName, qty);
                return 0;
            }
            Int16 CancelItem() // TBD 
            {
                return 0;
            }
            public Int16 PrintReceipt(Client _Client)
            {
                Console.WriteLine("RECEIPT " + DateTime.Now.ToString("h:mm:ss tt"));
                Console.WriteLine("\nName\tQty\tSubtotal\n");

                for (int j = 0; j < _Client._Receipt._Content.Count; j++)
                {
                    Console.Write(_Client._Receipt._Content[j].name + "\t");
                    Console.Write(_Client._Receipt._Content[j].quantity + "\t");
                    Console.WriteLine(_Client._Receipt._Content[j].price);
                }
                Console.WriteLine("\nTOTAL: " + _Client._Receipt.total + " PLN\n");
                return 0;
            }
        }

        class Shopping
        {
            static void Main(string[] args)
            {
                PriceList _PriceList = new PriceList();
                Casier _Casier = new Casier();

                // John 
                Client _ClientJohn = new Client();
                _Casier._Client = _ClientJohn;

                _Casier.ScanItem(_PriceList, _PriceList._Content[0].name); // Milk 1 10 = 10
                _Casier.ScanItem(_PriceList, _PriceList._Content[1].name, 9); // Water 8+1 4+6 = 38

                _Casier.PrintReceipt(_ClientJohn);

                // Bill
                Client _ClientBill = new Client();
                _Casier._Client = _ClientBill;

                _Casier.ScanItem(_PriceList, _PriceList._Content[2].name); // Sugar 1 15 = 15
                _Casier.ScanItem(_PriceList, _PriceList._Content[1].name, 10); // Water 8+2 4+6 = 44

                _Casier.PrintReceipt(_ClientBill);

                Console.ReadLine();
            }
        }
    }
}