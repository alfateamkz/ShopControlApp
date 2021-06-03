using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;

namespace ShopControlApp
{


    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public enum Tables
        {
            Checks = 1,
            DiscontCards = 2,
            Goods = 3,
            GoodsAtWarehouse = 4,
            Manufacturers = 5,
            Sellers = 6,
            Warehouses = 7
        } 

        public Tables table; //выбор таблицы, для switch(case) в командах
        public FilterParameter filterParameter; //условие фильтрации/поиска
        public FilterAction filterAction; //по возрастанию/убыванию или никак, если точное значение
        #region Конструкторы VievModel
        public ApplicationViewModel(Tables _table) //для crud
        {
            this.table = _table;  
        }
        public ApplicationViewModel(Tables _table, FilterAction _filterAction,
            FilterParameter _filterParameter = FilterParameter.Null) // для фильтрации/поиска
        {
            this.table = _table;
            this.filterAction = _filterAction;
            this.filterParameter = _filterParameter;
        }
        public ApplicationViewModel() { } //не помню, зачем, но пусть будет
        #endregion

        #region Свойства таблиц, в которые загружаются данные из бд в datagrid
        private ObservableCollection<Check> _Checks;
        public ObservableCollection<Check> Checks
        {
            get
            {
                return _Checks;
            }
            set
            {
                _Checks = value;
                OnPropertyChanged("Checks");
            }
        }
        private ObservableCollection<DiscontCard> _DiscontCards;
        public ObservableCollection<DiscontCard> DiscontCards
        {
            get
            {
                return _DiscontCards;
            }
            set
            {
                _DiscontCards = value;
                OnPropertyChanged("DiscontCards");
            }
        }
        private ObservableCollection<Warehouse> _Warehouses;
        public ObservableCollection<Warehouse> Warehouses
        {
            get
            {
                return _Warehouses;
            }
            set
            {
                _Warehouses = value;
                OnPropertyChanged("Warehouses");
            }
        }
        private ObservableCollection<Seller> _Sellers;
        public ObservableCollection<Seller> Sellers
        {
            get
            {
                return _Sellers;
            }
            set
            {
                _Sellers = value;
                OnPropertyChanged("Sellers");
            }
        }
        private ObservableCollection<Manufacturer> _Manufacturers;
        public ObservableCollection<Manufacturer> Manufacturers
        {
            get
            {
                return _Manufacturers;
            }
            set
            {
                _Manufacturers = value;
                OnPropertyChanged("Manufacturers");
            }
        }
        private ObservableCollection<GoodsAtWarehouses> _GoodsAtWarehouse;
        public ObservableCollection<GoodsAtWarehouses> GoodsAtWarehouse
        {
            get
            {
                return _GoodsAtWarehouse;
            }
            set
            {
                _GoodsAtWarehouse = value;
                OnPropertyChanged("GoodsAtWarehouse");
            }
        }
        private ObservableCollection<Product> _Goods;
        public ObservableCollection<Product> Goods
        {
            get
            {
                return _Goods;
            }
            set
            {
                _Goods = value;
                OnPropertyChanged("Goods");
            }
        }
        #endregion

        #region Начальное отображение содержимого таблиц // WIP !!!
        private DefaultCommand _LoadTable;
        public DefaultCommand LoadTable
        {
            get
            {
                return _LoadTable ?? (_LoadTable = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        switch (table)
                        {
                            case Tables.Checks:
                                Checks = new ObservableCollection<Check>(db.Checks.Include( s => s.Seller).Include(d => d.Discont));
                                break;
                            case Tables.DiscontCards:
                                DiscontCards = new ObservableCollection<DiscontCard>(db.DiscontCards);
                                break;
                            case Tables.Goods:
                                Goods = new ObservableCollection<Product>(db.Goods.Include(a => a.Manufacturer));
                                break;
                            case Tables.GoodsAtWarehouse:
                                GoodsAtWarehouse = new ObservableCollection<GoodsAtWarehouses>(db.GoodsAtWarehouse.Where(a => a.WarehouseID == SelectedWarehouse.ID)
                                    .Include(g => g.Goods).Include(w => w.Warehouse));
                                break;
                            case Tables.Manufacturers:
                                Manufacturers = new ObservableCollection<Manufacturer>(db.Manufacturers);
                                break;
                            case Tables.Sellers:
                                Sellers = new ObservableCollection<Seller>(db.Sellers.Include(p => p.Position));
                                break;
                            case Tables.Warehouses:
                                Warehouses = new ObservableCollection<Warehouse>(db.Warehouses);
                                break;
                            default:
                                break;
                        }
                    }
                }));
            }
        }
        #endregion

        #region Авторизация
        private MainMenu MainMenu; // главное меню
        private DefaultCommand _AuthCommand;
        public DefaultCommand AuthCommand
        {
            get
            {
                return _AuthCommand ?? (_AuthCommand = new DefaultCommand(o =>
                {
                    Credentials credentials = new Credentials(Username, Password);
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        var sellers = from a in db.Sellers select new { Username = a.Username, Password = a.Password, Position = a.PositionID,SellerID = a.ID};
                        Seller seller = new Seller();
                        seller.Username = credentials.Username;
                        seller.Password = credentials.Password;
                        bool isSuccesful = false;
                        foreach (var i in sellers)
                        {
                            if (seller.Password == i.Password && seller.Username == i.Username)
                            {
                                isSuccesful = true;
                                EmployeeID = i.SellerID;
                                MainMenu = new MainMenu(i.Position); MainMenu.Show();
                                break;
                            }
                        }
                        if (isSuccesful == false)
                        {
                            MsgBox f = new MsgBox(MessageCode.AuthError);
                            

                        }
                    }
                }
                ));
            }
        }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        private static int _EmployeeID;
        public int EmployeeID
        {
            get
            {
                return _EmployeeID;
            }
            set
            {
                _EmployeeID = value;
                OnPropertyChanged("EmployeeID");
            }
        }

        class Credentials
        {
            public string Username { get; set; }
            public string Password { get; set; }

            public Credentials(string username, string password)
            {
                this.Password = password;
                this.Username = username;
            }
        }
        #endregion

        #region Свойства INotifyPropertyChanged для действий добавления, удаления и обновления
        /// <summary>
        /// свойства для передачи параметров в команду 
        /// свойства классов предназначены для передачи выделенного объекта из combobox в команду
        /// </summary>

        private Position _SelectedPosition;
        public Position SelectedPosition
        {
            get
            {
                return _SelectedPosition;
            }
            set
            {
                _SelectedPosition = value;
                OnPropertyChanged("SelectedPosition");
            }
        }
        private Manufacturer _GoodsManufacturer; // берется при выборе из combobox
        public Manufacturer GoodsManufacturer
        {
            get
            {
                return _GoodsManufacturer;
            }
            set
            {
                _GoodsManufacturer = value;
                OnPropertyChanged("GoodsManufacturer");
            }
        }
        #region Чеки
        private DateTime _CheckDate;
        public DateTime CheckDate
        {
            get
            {
                return _CheckDate;
            }
            set
            {
                _CheckDate = value;
                OnPropertyChanged("CheckDate");
            }
        }
        #endregion
        #region Дисконтные карты

        private string _DiscontCardPhone;
        public string DiscontCardPhone
        {
            get
            {
                return _DiscontCardPhone;
            }
            set
            {
                _DiscontCardPhone = value;
                OnPropertyChanged("DiscontCardPhone");
            }
        }
        private double _DiscontCardPercentage;
        public double DiscontCardPercentage
        {
            get
            {
                return _DiscontCardPercentage;
            }
            set
            {
                _DiscontCardPercentage = value;
                OnPropertyChanged("DiscontCardPercentage");
            }
        }
        private double _DiscontCardSum;
        public double DiscontCardSum
        {
            get
            {
                return _DiscontCardSum;
            }
            set
            {
                _DiscontCardSum = value;
                OnPropertyChanged("DiscontCardSum");
            }
        }
        private int _DiscontCardID;
        public int DiscontCardID
        {
            get
            {
                return _DiscontCardID;
            }
            set
            {
                _DiscontCardID = value;
                OnPropertyChanged("DiscontCardID");
            }
        }
        private DiscontCard _SelectedDiscontCard;
        public DiscontCard SelectedDiscontCard
        {
            get
            {
                return _SelectedDiscontCard;
            }
            set
            {
                _SelectedDiscontCard = value;
                OnPropertyChanged("SelectedDiscontCard");
            }
        }

        #endregion
        #region Товары (goods)
        private int _GoodsID;
        public int GoodsID
        {
            get
            {
                return _GoodsID;
            }
            set
            {
                _GoodsID = value;
                OnPropertyChanged("GoodsID");
            }
        }
        private string _GoodsTitle;
        public string GoodsTitle
        {
            get
            {
                return _GoodsTitle;
            }
            set
            {
                _GoodsTitle = value;
                OnPropertyChanged("GoodsTitle");
            }
        }
        private double _GoodsPrice;
        public double GoodsPrice
        {
            get
            {
                return _GoodsPrice;
            }
            set
            {
                _GoodsPrice = value;
                OnPropertyChanged("GoodsPrice");
            }
        }
        private string _GoodsWarranty;
        public string GoodsWarranty
        {
            get
            {
                return _GoodsWarranty;
            }
            set
            {
                _GoodsWarranty = value;
                OnPropertyChanged("GoodsWarranty");
            }
        }
        private int _GoodsManufacturerID;
        public int GoodsManufacturerID
        {
            get
            {
                return _GoodsManufacturerID;
            }
            set
            {
                _GoodsManufacturerID = value;
                OnPropertyChanged("GoodsManufacturerID");
            }
        }
        private Product _SelectedGood;
        public Product SelectedGood
        {
            get
            {
                return _SelectedGood;
            }
            set
            {
                _SelectedGood = value;
                OnPropertyChanged("SelectedGood");
            }
        }
        #endregion
        #region Товары на складе

        ///уже добавленный товар на складе
        private int _GoodsAtWarehouseID;
        public int GoodsAtWarehouseID
        {
            get
            {
                return _GoodsAtWarehouseID;
            }
            set
            {
                _GoodsAtWarehouseID = value;
                OnPropertyChanged("GoodsAtWarehouseID");
            }
        }

        /// <summary>
        /// товар который надо добавить на конкретный склад
        /// </summary>
        private int _GoodsAtWarehouseProductID;
        public int GoodsAtWarehouseProductID
        {
            get
            {
                return _GoodsAtWarehouseProductID;
            }
            set
            {
                _GoodsAtWarehouseProductID = value;
                OnPropertyChanged("GoodsAtWarehouseProductID");
            }
        }
        private int _GoodsAtWarehouseQuantity;
        public int GoodsAtWarehouseQuantity
        {
            get
            {
                return _GoodsAtWarehouseQuantity;
            }
            set
            {
                _GoodsAtWarehouseQuantity = value;
                OnPropertyChanged("GoodsAtWarehouseQuantity");
            }
        }
        #endregion
        #region Производители

        private int _ManufacturerID;
        public int ManufacturerID
        {
            get
            {
                return _ManufacturerID;
            }
            set
            {
                _ManufacturerID = value;
                OnPropertyChanged("ManufacturerID");
            }
        }
        private string _ManufacturerTitle;
        public string ManufacturerTitle
        {
            get
            {
                return _ManufacturerTitle;
            }
            set
            {
                _ManufacturerTitle = value;
                OnPropertyChanged("ManufacturerTitle");
            }
        }
        private string _ManufacturerCountry;
        public string ManufacturerCountry
        {
            get
            {
                return _ManufacturerCountry;
            }
            set
            {
                _ManufacturerCountry = value;
                OnPropertyChanged("ManufacturerCountry");
            }
        }
        private string _ManufacturerCity;
        public string ManufacturerCity
        {
            get
            {
                return _ManufacturerCity;
            }
            set
            {
                _ManufacturerCity = value;
                OnPropertyChanged("ManufacturerCity");
            }
        }

        #endregion
        #region персонал (sellers)
        private int _SellerID;
        public int SellerID
        {
            get
            {
                return _SellerID;
            }
            set
            {
                _SellerID = value;
                OnPropertyChanged("SellerID");
            }
        }
        private string _SellerName;
        public string SellerName
        {
            get
            {
                return _SellerName;
            }
            set
            {
                _SellerName = value;
                OnPropertyChanged("SellerName");
            }
        }
        private string _SellerPosition;
        public string SellerPosition
        {
            get
            {
                return _SellerPosition;
            }
            set
            {
                _SellerPosition = value;
                OnPropertyChanged("SellerPosition");
            }
        }
        private string _SellerSurname;
        public string SellerSurname
        {
            get
            {
                return _SellerSurname;
            }
            set
            {
                _SellerSurname = value;
                OnPropertyChanged("SellerSurname");
            }
        }
        private string _SellerPatronymic;
        public string SellerPatronymic
        {
            get
            {
                return _SellerPatronymic;
            }
            set
            {
                _SellerPatronymic = value;
                OnPropertyChanged("SellerPatronymic");
            }
        }
        private DateTime _SellerBirthday;
        public DateTime SellerBirthday
        {
            get
            {
                return _SellerBirthday;
            }
            set
            {
                _SellerBirthday = value;
                OnPropertyChanged("SellerBirthday");
            }
        }
        private DateTime _SellerEmploymentDay;
        public DateTime SellerEmploymentDay
        {
            get
            {
                return _SellerEmploymentDay;
            }
            set
            {
                _SellerEmploymentDay = value;
                OnPropertyChanged("SellerEmploymentDay");
            }
        }
        private int _SellerPositionID;
        public int SellerPositionID
        {
            get
            {
                return _SellerPositionID;
            }
            set
            {
                _SellerPositionID = value;
                OnPropertyChanged("SellerPositionID");
            }
        }
        private string _SellerUsername;
        public string SellerUsername
        {
            get
            {
                return _SellerUsername;
            }
            set
            {
                _SellerUsername = value;
                OnPropertyChanged("SellerUsername");
            }
        }
        private string _SellerPassword;
        public string SellerPassword
        {
            get
            {
                return _SellerPassword;
            }
            set
            {
                _SellerPassword = value;
                OnPropertyChanged("SellerPassword");
            }
        }

        private Seller _SelectedSeller;
        public Seller SelectedSeller
        {
            get
            {
                return _SelectedSeller;
            }
            set
            {
                _SelectedSeller = value;
                OnPropertyChanged("SelectedSeller");
            }
        }
        #endregion
        #region склады

        private int _WarehouseID;
        public int WarehouseID
        {
            get
            {
                return _WarehouseID;
            }
            set
            {
                _WarehouseID = value;
                OnPropertyChanged("WarehouseID");
            }
        }
        private string _WarehouseAddress;
        public string WarehouseAddress
        {
            get
            {
                return _WarehouseAddress;
            }
            set
            {
                _WarehouseAddress = value;
                OnPropertyChanged("WarehouseAddress");
            }
        }
        private Warehouse _SelectedWarehouse;
        public Warehouse SelectedWarehouse
        {
            get
            {
                return _SelectedWarehouse;
            }
            set
            {
                _SelectedWarehouse = value;
                OnPropertyChanged("SelectedWarehouse");
            }
        }
        #endregion
        #endregion

        #region Команда добавления записей в БД
        private DefaultCommand _InsertCommand;
        public DefaultCommand InsertCommand
        {
            get
            {
                return _InsertCommand ?? (_InsertCommand = new DefaultCommand(o =>
                {

                    using (DatabaseContext db = new DatabaseContext())
                    {
                        switch (table)
                        {
                            case Tables.DiscontCards:
                                try
                                {
                                    DiscontCard discontCard = new DiscontCard { Percentage = 1, Sum = 0, Phone = DiscontCardPhone };
                                    db.DiscontCards.Add(discontCard);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulAdd); 
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.NullError);
                                }
                                break;
                            case Tables.Goods:
                                try
                                {
                                    Product product = new Product
                                    {
                                        ManufacturerID = GoodsManufacturer.ID,
                                        Price = GoodsPrice,
                                        Title = GoodsTitle,
                                        Warranty = GoodsWarranty
                                    };
                                    db.Goods.Add(product);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulAdd); 
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.NullError);
                                }
                                break;
                            case Tables.GoodsAtWarehouse:
                                try
                                {
                                    GoodsAtWarehouses goods = new GoodsAtWarehouses
                                    {
                                        WarehouseID = SelectedWarehouse.ID,
                                        GoodsID = SelectedGood.ID,
                                        Quantity = GoodsAtWarehouseQuantity
                                    };
                                    db.GoodsAtWarehouse.Add(goods);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulAdd);
                                    ShopControlApp.GoodsAtWarehouse.GoodsAtWarehouse f = new ShopControlApp.GoodsAtWarehouse.GoodsAtWarehouse();
                                    f.DataContext = this;
                                    f.Show();
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.NullError);
                                }
                                break;
                            default:
                                break;
                            case Tables.Manufacturers:
                                try
                                {
                                    Manufacturer manufacturer = new Manufacturer
                                    {
                                        Country = ManufacturerCountry,
                                        City = ManufacturerCity,
                                        Title = ManufacturerTitle
                                    };
                                    db.Manufacturers.Add(manufacturer);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulAdd);

                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulAdd); 
                                }
                                break;
                            case Tables.Sellers:
                                try
                                {
                                    Seller seller = new Seller
                                    {
                                        Name = SellerName,
                                        Surname = SellerSurname,
                                        Patronymic = SellerPatronymic,
                                        Password = SellerPassword,
                                        Username = SellerUsername,
                                        Birthday = SellerBirthday,
                                        EmploymentDay = SellerEmploymentDay,
                                        PositionID = SelectedPosition.ID
                                    };
                                    db.Sellers.Add(seller);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulAdd); 
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulAdd);
                                }
                                break;
                            case Tables.Warehouses:
                                try
                                {
                                    Warehouse warehouse = new Warehouse { Address = WarehouseAddress };
                                    db.Warehouses.Add(warehouse);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulAdd); 
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.NullError);
                                }
                                break;
                        }

                    }

                }));
            }
        }
        #endregion
        #region Команда удаления объектов из БД
        private DefaultCommand _DeleteCommand;
        public DefaultCommand DeleteCommand
        {
            get
            {
                return _DeleteCommand ?? (_DeleteCommand = new DefaultCommand(o =>
                {

                    using (DatabaseContext db = new DatabaseContext())
                    {
                        switch (table)
                        {
                            case Tables.DiscontCards:
                                try
                                {
                                    DiscontCard discontCard = db.DiscontCards.Where(a => a.ID == DiscontCardID).First();
                                    db.DiscontCards.Remove(discontCard);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulDelete);
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.UnsuccessfulDelete);
                                }
                                break;
                            case Tables.Goods:
                                try
                                {
                                    Product product = db.Goods.Where(a => a.ID == GoodsID).First();
                                    db.Goods.Remove(product);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulDelete); 
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.UnsuccessfulDelete);
                                }
                                break;
                            case Tables.GoodsAtWarehouse:
                                try
                                {
                                    GoodsAtWarehouses product = db.GoodsAtWarehouse.Where(a => a.ID == GoodsAtWarehouseID).First();
                                    db.GoodsAtWarehouse.Remove(product);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulDelete);
                                    ShopControlApp.GoodsAtWarehouse.GoodsAtWarehouse f = new GoodsAtWarehouse.GoodsAtWarehouse();
                                    f.DataContext = this;
                                    f.Show();
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.UnsuccessfulDelete); 
                                }
                                break;
                            case Tables.Manufacturers:
                                try
                                {
                                    Manufacturer manufacturer = db.Manufacturers.Where(a => a.ID == ManufacturerID).First();
                                    db.Manufacturers.Remove(manufacturer);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulDelete); 
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.UnsuccessfulDelete); 
                                }
                                break;
                            case Tables.Sellers:
                                try
                                {
                                    Seller seller = db.Sellers.Where(a => a.ID == SellerID).First();
                                    db.Sellers.Remove(seller);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulDelete); 
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.UnsuccessfulDelete);
                                }
                                break;
                            case Tables.Warehouses:
                                try
                                {
                                    Warehouse warehouse = db.Warehouses.Where(a => a.ID == WarehouseID).First();
                                    db.Warehouses.Remove(warehouse);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulDelete); 
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.UnsuccessfulDelete);
                                }
                                break;
                        }
                    }
                }));
            }
        }
        #endregion
        #region Команда обновления записей в БД
        private DefaultCommand _UpdateCommand;

        public DefaultCommand UpdateCommand
        {
            get
            {
                return _UpdateCommand ?? (_UpdateCommand = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        switch (table)
                        {
                            case Tables.DiscontCards:
                                try
                                {
                                    DiscontCard card = db.DiscontCards.Where(a => a.ID == SelectedDiscontCard.ID).First();
                                    card.Phone = DiscontCardPhone;
                                    if (card != null)
                                    {
                                        db.DiscontCards.Update(card);
                                        db.SaveChanges();
                                        MsgBox msgBox = new MsgBox(MessageCode.SuccessfulUpdate);
                                    }
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.NullError);
                                }
                                break;
                            case Tables.Goods:
                                try
                                {
                                    Product product = db.Goods.Where(a => a.ID == SelectedGood.ID).First();
                                    product.ManufacturerID = GoodsManufacturer.ID;
                                    product.Price = GoodsPrice;
                                    product.Title = GoodsTitle;
                                    product.Warranty = GoodsWarranty;

                                    db.Goods.Update(product);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulUpdate);
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.NullError);
                                }
                                break;
                            case Tables.GoodsAtWarehouse:
                                try
                                {
                                    GoodsAtWarehouses goods = db.GoodsAtWarehouse.Where(a => a.ID == GoodsAtWarehouseID).First();
                                    goods.WarehouseID = SelectedWarehouse.ID;
                                    goods.Quantity = GoodsAtWarehouseQuantity;
                                    db.GoodsAtWarehouse.Update(goods);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulUpdate);
                                    ShopControlApp.GoodsAtWarehouse.GoodsAtWarehouse f = new ShopControlApp.GoodsAtWarehouse.GoodsAtWarehouse();
                                    f.DataContext = this;
                                    f.Show();
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.NullError);
                                }
                                break;
                            case Tables.Manufacturers:
                                try
                                {
                                    Manufacturer manufacturer = db.Manufacturers.Where(a => a.ID == GoodsManufacturer.ID).First();
                                    {
                                        manufacturer.Country = ManufacturerCountry;
                                        manufacturer.City = ManufacturerCity;
                                        manufacturer.Title = ManufacturerTitle;
                                    };
                                    db.Manufacturers.Update(manufacturer);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulUpdate);
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.NullError);
                                }

                                break;
                            case Tables.Sellers:
                                try
                                {
                                    Seller seller = db.Sellers.Where(a => a.ID == SelectedSeller.ID).First();
                                    seller.Name = SellerName;
                                    seller.Surname = SellerSurname;
                                    seller.Patronymic = SellerPatronymic;
                                    seller.Password = SellerPassword;
                                    seller.Username = SellerUsername;
                                    seller.Birthday = SellerBirthday;
                                    seller.EmploymentDay = SellerEmploymentDay;
                                    seller.PositionID = SelectedPosition.ID;
                                    db.Sellers.Update(seller);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulUpdate); 
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.NullError);
                                }
                                break;
                            case Tables.Warehouses:
                                try
                                {
                                    Warehouse warehouse = db.Warehouses.Where(a => a.ID == SelectedWarehouse.ID).First();
                                    warehouse.Address = WarehouseAddress;
                                    db.Warehouses.Update(warehouse);
                                    db.SaveChanges();
                                    MsgBox msgBox = new MsgBox(MessageCode.SuccessfulUpdate); 
                                }
                                catch
                                {
                                    MsgBox msgBox = new MsgBox(MessageCode.NullError); 
                                }
                                break;
                        }
                    }
                }));
            }
        }
        #endregion
     
        #region Загрузка листов выбора
        /// <summary>
        /// Предназначено для отображения списка выбора. К примеру выбор производителя 
        /// при создании нового товара
        /// </summary>
        #region лист произодителей
     

        private DefaultCommand _loadManufacturersID;
        public DefaultCommand LoadManufacturersID
        {
            get
            {
                return _loadManufacturersID ?? (_loadManufacturersID = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {

                        var queryable = from i in db.Manufacturers select i;
                        Manufacturers = new ObservableCollection<Manufacturer>();
                        foreach (var i in queryable)
                        {
                            Manufacturers.Add(i);
                        }
                    }
                }));
            }
        }
        #endregion
        #region лист складов

        private DefaultCommand _loadWarehousesID;
        public DefaultCommand LoadWarehousesID
        {
            get
            {
                return _loadWarehousesID ?? (_loadWarehousesID = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {

                        var queryable = from i in db.Warehouses select i;
                        Warehouses = new ObservableCollection<Warehouse>();
                        foreach (var i in queryable)
                        {
                            Warehouses.Add(i);
                        }

                    }
                }));
            }
        }
        #endregion
        #region лист продуктов
        private DefaultCommand _loadGoodsID;
        public DefaultCommand LoadGoodsID
        {
            get
            {
                return _loadGoodsID ?? (_loadGoodsID = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        var queryable = from i in db.Goods select i;
                        Goods = new ObservableCollection<Product>();
                        foreach (var i in queryable)
                        {
                            Goods.Add(i);
                        }
                    }
                }));
            }
        }
        #endregion
        #region лист должностей

        private ObservableCollection<Position> _PositionsList;

        public ObservableCollection<Position> PositionsList
        {
            get
            {
                return _PositionsList;
            }
            set
            {
                _PositionsList = value;
                OnPropertyChanged("PositionsList");
            }
        }

        private DefaultCommand _LoadPositionID;
        public DefaultCommand LoadPositionID
        {
            get
            {
                return _LoadPositionID ?? (_LoadPositionID = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {

                        var queryable = from i in db.Positions select new { ID = i.ID, Title = i.Title };
                        PositionsList = new ObservableCollection<Position>();
                        foreach (var i in queryable)
                        {
                            PositionsList.Add(new Position { ID = i.ID, Title = i.Title });
                        }

                    }
                }));
            }
        }
        #endregion
        #region Лист дисконтных карт

        private DefaultCommand _LoadDiscontCardID;
        public DefaultCommand LoadDiscontCardID
        {
            get
            {
                return _LoadDiscontCardID ?? (_LoadDiscontCardID = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        var queryable = from i in db.DiscontCards select i;
                        DiscontCards = new ObservableCollection<DiscontCard>();
                        foreach (var i in queryable)
                        {
                            DiscontCards.Add(i);
                        }
                    }
                }));
            }
        }
        #endregion
        #endregion

        #region Оформление покупки

        #region лист продуктов на складе
        public class WarehouseProduct
        {
            public string Title { get; set; }
            public int ID { get; set; }
            public int ProductID { get; set; }
            public int Quantity { get; set; }

            public override string ToString()
            {
                return $"{Title}  Количество на складе : {Quantity}";
            }
        }

        private ObservableCollection<WarehouseProduct> _GoodsAtWarehouseList;

        public ObservableCollection<WarehouseProduct> GoodsAtWarehouseList
        {
            get
            {
                return _GoodsAtWarehouseList;
            }
            set
            {
                _GoodsAtWarehouseList = value;
                OnPropertyChanged("GoodsAtWarehouseList");
            }
        }

        private DefaultCommand _loadGoodsAtWarehouseID;
        public DefaultCommand LoadGoodsAtWarehouseID
        {
            get
            {
                BasketList = new ObservableCollection<WarehouseProduct>();
                return _loadGoodsAtWarehouseID ?? (_loadGoodsAtWarehouseID = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {

                        var queryable = from i in db.GoodsAtWarehouse.Where(w => w.WarehouseID == SelectedWarehouse.ID)
                                        select new { ID = i.ID, GoodsID = i.GoodsID, Title = i.Goods.Title, Quantity = i.Quantity };
                        GoodsAtWarehouseList = new ObservableCollection<WarehouseProduct>();
                        foreach (var i in queryable)
                        {
                            GoodsAtWarehouseList.Add(new WarehouseProduct { ID = i.ID, Title = i.Title, ProductID = i.GoodsID, Quantity = i.Quantity });
                        }
                        BasketList.Clear();
                    }
                }));
            }
        }
        private WarehouseProduct _SelectedGoodsAtWarehouse;
        public WarehouseProduct SelectedGoodsAtWarehouse
        {
            get
            {
                return _SelectedGoodsAtWarehouse;
            }
            set
            {
                _SelectedGoodsAtWarehouse = value;
                OnPropertyChanged("SelectedGoodsAtWarehouse");
            }
        }
        #endregion

        private ObservableCollection<WarehouseProduct> _BasketList;

        public ObservableCollection<WarehouseProduct> BasketList
        {
            get
            {
                return _BasketList;
            }
            set
            {
                _BasketList = value;
                OnPropertyChanged("BasketList");
            }
        }


        private int _SelectedBasketItemQuantity;
        public int SelectedBasketItemQuantity
        {
            get
            {
                return _SelectedBasketItemQuantity;
            }
            set
            {
                _SelectedBasketItemQuantity = value;
                OnPropertyChanged("SelectedBasketItemQuantity");
            }
        }

        private DefaultCommand _BasketAdd;
        public DefaultCommand BasketAdd
        {
            get
            {
                BasketList = new ObservableCollection<WarehouseProduct>();
                return _BasketAdd ?? (_BasketAdd = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        try
                        {
                            if (SelectedGoodsAtWarehouse != null)
                            {
                                if (SelectedBasketItemQuantity > SelectedGoodsAtWarehouse.Quantity)
                                {
                                    MsgBox f = new MsgBox(MessageCode.NotEnoughAtWarehouse); 
                                }
                                else if (SelectedBasketItemQuantity <= 0)
                                {
                                    MsgBox f = new MsgBox(MessageCode.WrongBasketQuantity);
                                }
                                else
                                {
                                    WarehouseProduct item = SelectedGoodsAtWarehouse;
                                    item.Quantity = SelectedBasketItemQuantity;
                                    GoodsAtWarehouseList.Remove(SelectedGoodsAtWarehouse);
                                    BasketList.Add(item);
                                }
                            }
                        }
                        catch 
                        {
                           
                          MsgBox f = new MsgBox(MessageCode.NullError); 
                        }
                    }
                }));
            }

        }
        private DefaultCommand _Сheckout
;
        public DefaultCommand Сheckout
        {
            get
            {
                return _Сheckout ?? (_Сheckout = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        try
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            foreach (var i in BasketList)
                            {
                                stringBuilder.Append(i.Title + " количество : " + i.Quantity + ",");
                            }
                            if (SelectedDiscontCard != null)
                            {
                                db.Checks.Add(new Check
                                {
                                    SellerID = _EmployeeID,
                                    DiscontID = SelectedDiscontCard.ID,
                                    SellDate = DateTime.Now,
                                    ListOfProducts = stringBuilder.ToString()
                                });
                            }
                            else
                            {
                                db.Checks.Add(new Check
                                {
                                    SellerID = _EmployeeID,
                                    SellDate = DateTime.Now,
                                    ListOfProducts = stringBuilder.ToString()
                                });
                            }
                            db.SaveChanges();
                            MsgBox f = new MsgBox(MessageCode.CheckoutEnd); 

                        }
                        catch
                        {
                            MsgBox f = new MsgBox(MessageCode.NullError);
                        }
                    }
                }));
            }
        }
        #endregion


        ///
        /// Код смены стилей находится в Settings.xaml.cs
        ///

        public enum FilterAction // условие фильтрации/поиска
        {
            ByDate = 1,
            BySum = 2,
            ByPercent = 3,
            ByPrice = 4,
            ByName = 5,
            ByWarranty = 6,
            ByCountry = 7,
            ByBirthDay = 8,
            ByEmploymentDate = 9,
            BySurname = 10,
            ByPatronymic = 11,
            ByPosition = 12,
            ByTitle = 13,
            ByAddress = 14
        } 
        public enum FilterParameter // возрастание/убывание
        {
            Acs = 1,
            Decs = 2,
            Null = 3
        } 
        

        public enum Lang
        {
            Russian = 1,
            English = 2
        }
        #region Фильтрация
        private DefaultCommand _LoadFilteredTable;
        public DefaultCommand LoadFilteredTable
        {
            get
            {
                return _LoadFilteredTable ?? (_LoadFilteredTable = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        switch (table)
                        {
                            case Tables.Checks:
                                switch (filterAction)
                                {
                                    case FilterAction.ByDate:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Checks = new ObservableCollection<Check>(db.Checks.OrderBy(i => i.SellDate)
                                                    .Include(s => s.Seller).Include(d => d.Discont));
                                                break;
                                            case FilterParameter.Decs:
                                                Checks = new ObservableCollection<Check>(db.Checks.OrderByDescending(i => i.SellDate)
                                                    .Include(s => s.Seller).Include(d => d.Discont));
                                                break;
                                        }
                                        break;
                                }                     
                                break;
                            case Tables.DiscontCards:
                                switch (filterAction)
                                {
                                    case FilterAction.ByPercent:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                DiscontCards = new ObservableCollection<DiscontCard>(db.DiscontCards.OrderBy(i => i.Percentage));
                                                break;
                                            case FilterParameter.Decs:
                                                DiscontCards = new ObservableCollection<DiscontCard>(db.DiscontCards.OrderByDescending(i => i.Percentage));
                                                break;
                                        }
                                        break;
                                    case FilterAction.BySum:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                DiscontCards = new ObservableCollection<DiscontCard>(db.DiscontCards.OrderBy(i => i.Sum));
                                                break;
                                            case FilterParameter.Decs:
                                                DiscontCards = new ObservableCollection<DiscontCard>(db.DiscontCards.OrderByDescending(i => i.Sum));
                                                break;
                                        }
                                        break;
                                }
                                break;
                            case Tables.Goods:
                                switch (filterAction)
                                {
                                    case FilterAction.ByTitle:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Goods = new ObservableCollection<Product>(db.Goods.OrderBy(i => i.Title).Include(a => a.Manufacturer));
                                                break;
                                            case FilterParameter.Decs:
                                                Goods = new ObservableCollection<Product>(db.Goods.OrderByDescending(i => i.Title).Include(a => a.Manufacturer));
                                                break;
                                        }
                                        break;
                                    case FilterAction.ByPrice:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Goods = new ObservableCollection<Product>(db.Goods.OrderBy(i => i.Price).Include(a => a.Manufacturer));
                                                break;
                                            case FilterParameter.Decs:
                                                Goods = new ObservableCollection<Product>(db.Goods.OrderByDescending(i => i.Price).Include(a => a.Manufacturer));
                                                break;
                                        }
                                        break;
                                    case FilterAction.ByWarranty:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Goods = new ObservableCollection<Product>(db.Goods.OrderBy(i => i.Warranty).Include(a => a.Manufacturer));
                                                break;
                                            case FilterParameter.Decs:
                                                Goods = new ObservableCollection<Product>(db.Goods.OrderByDescending(i => i.Warranty).Include(a => a.Manufacturer));
                                                break;
                                        }
                                        break;
                                }                                        
                                break;                           
                            case Tables.Manufacturers:
                                switch (filterAction)
                                {
                                    case FilterAction.ByTitle:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Manufacturers = new ObservableCollection<Manufacturer>(db.Manufacturers.OrderBy(i => i.Title));
                                                break;
                                            case FilterParameter.Decs:
                                                Manufacturers = new ObservableCollection<Manufacturer>(db.Manufacturers.OrderByDescending(i => i.Title));
                                                break;
                                        }
                                        break;
                                    case FilterAction.ByCountry:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Manufacturers = new ObservableCollection<Manufacturer>(db.Manufacturers.OrderBy(i => i.Country));
                                                break;
                                            case FilterParameter.Decs:
                                                Manufacturers = new ObservableCollection<Manufacturer>(db.Manufacturers.OrderByDescending(i => i.Country));
                                                break;
                                        }
                                        break;
                                }
                                break;
                            case Tables.Sellers:
                                switch (filterAction)
                                {
                                    case FilterAction.ByBirthDay:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderBy(i => i.Birthday).Include(p => p.Position));
                                                break;
                                            case FilterParameter.Decs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderByDescending(i => i.Birthday).Include(p => p.Position));
                                                break;
                                        }
                                        break;
                                    case FilterAction.ByEmploymentDate:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderBy(i => i.EmploymentDay).Include(p => p.Position));
                                                break;
                                            case FilterParameter.Decs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderByDescending(i => i.EmploymentDay).Include(p => p.Position));
                                                break;
                                        }
                                        break;
                                    case FilterAction.ByName:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderBy(i => i.Name).Include(p => p.Position));
                                                break;
                                            case FilterParameter.Decs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderByDescending(i => i.Name).Include(p => p.Position));
                                                break;
                                        }
                                        break;
                                    case FilterAction.BySurname:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderBy(i => i.Surname).Include(p => p.Position));
                                                break;
                                            case FilterParameter.Decs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderByDescending(i => i.Surname).Include(p => p.Position));
                                                break;
                                        }
                                        break;
                                    case FilterAction.ByPatronymic:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderBy(i => i.Patronymic).Include(p => p.Position));
                                                break;
                                            case FilterParameter.Decs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderByDescending(i => i.Patronymic).Include(p => p.Position));
                                                break;
                                        }
                                        break;
                                    case FilterAction.ByPosition:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderBy(i => i.Position).Include(p => p.Position));
                                                break;
                                            case FilterParameter.Decs:
                                                Sellers = new ObservableCollection<Seller>(db.Sellers.OrderByDescending(i => i.Position).Include(p => p.Position));
                                                break;
                                        }
                                        break;
                                }
                                break;
                            case Tables.Warehouses:
                                switch (filterAction)
                                {
                                    case FilterAction.ByAddress:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Warehouses = new ObservableCollection<Warehouse>(db.Warehouses.OrderBy(i => i.Address));
                                                break;
                                            case FilterParameter.Decs:
                                                Warehouses = new ObservableCollection<Warehouse>(db.Warehouses.OrderByDescending(i => i.Address));
                                                break;
                                        }
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }));
            }
        }
        #endregion
        #region Поиск
        private DefaultCommand _LoadSearch;
        public DefaultCommand LoadSearch
        {
            get
            {
                return _LoadSearch ?? (_LoadSearch = new DefaultCommand(o =>
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        switch (table)
                        {
                            case Tables.Checks:
                                switch (filterAction)
                                {
                                    case FilterAction.ByDate: 
                                     Checks = new ObservableCollection<Check>(db.Checks.Where(i => i.SellDate.Date == CheckDate.Date)
                                         .Include(s => s.Seller).Include(d => d.Discont));
                                    break;
                                }
                                break;
                            case Tables.DiscontCards:
                                switch (filterAction)
                                {
                                    case FilterAction.ByPercent:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                DiscontCards = new ObservableCollection<DiscontCard>(db.DiscontCards.Where(i => i.Percentage >= DiscontCardPercentage));
                                                break;
                                            case FilterParameter.Decs:
                                                DiscontCards = new ObservableCollection<DiscontCard>(db.DiscontCards.Where(i => i.Percentage <= DiscontCardPercentage));
                                                break;
                                        }
                                        break;
                                    case FilterAction.BySum:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                DiscontCards = new ObservableCollection<DiscontCard>(db.DiscontCards.Where(i => i.Sum >= DiscontCardSum));
                                                break;
                                            case FilterParameter.Decs:
                                                DiscontCards = new ObservableCollection<DiscontCard>(db.DiscontCards.Where(i => i.Sum <= DiscontCardSum));
                                                break;
                                        }
                                        break;
                                }
                                break;
                            case Tables.Goods:
                                switch (filterAction)
                                {
                                    case FilterAction.ByTitle:
                                        Goods = new ObservableCollection<Product>(db.Goods.Where(i => i.Title == GoodsTitle).Include(a => a.Manufacturer));
                                        break;
                                    case FilterAction.ByPrice:
                                        switch (filterParameter)
                                        {
                                            case FilterParameter.Acs:
                                                Goods = new ObservableCollection<Product>(db.Goods.Where(i => i.Price >= GoodsPrice).Include(a => a.Manufacturer));
                                                break;
                                            case FilterParameter.Decs:
                                                Goods = new ObservableCollection<Product>(db.Goods.Where(i => i.Price <= GoodsPrice).Include(a => a.Manufacturer));
                                                break;
                                        }
                                        break;
                                    case FilterAction.ByWarranty:
                                         Goods = new ObservableCollection<Product>(db.Goods.Where(i => i.Warranty == GoodsWarranty).Include(a => a.Manufacturer));
                                        break;
                                }
                                break;
                            case Tables.Manufacturers:
                                switch (filterAction)
                                {
                                    case FilterAction.ByTitle:
                                        Manufacturers = new ObservableCollection<Manufacturer>(db.Manufacturers.Where(i => i.Title == ManufacturerTitle));
                                        break;
                                    case FilterAction.ByCountry:
                                        Manufacturers = new ObservableCollection<Manufacturer>(db.Manufacturers.Where(i => i.Country == ManufacturerCountry));
                                        break;
                                }
                                break;
                            case Tables.Sellers:
                                switch (filterAction)
                                {
                                    case FilterAction.ByBirthDay:
                                        Sellers = new ObservableCollection<Seller>(db.Sellers.Where(i => i.Birthday.Date == SellerBirthday.Date).Include(p => p.Position));
                                        break;
                                    case FilterAction.ByEmploymentDate:
                                        Sellers = new ObservableCollection<Seller>(db.Sellers.Where(i => i.EmploymentDay.Date == SellerEmploymentDay.Date).Include(p => p.Position));
                                        break;
                                    case FilterAction.ByName:
                                        Sellers = new ObservableCollection<Seller>(db.Sellers.Where(i => i.Name == SellerName).Include(p => p.Position));
                                        break;
                                    case FilterAction.BySurname:
                                        Sellers = new ObservableCollection<Seller>(db.Sellers.Where(i => i.Surname == SellerSurname).Include(p => p.Position));
                                        break;
                                    case FilterAction.ByPatronymic:
                                        Sellers = new ObservableCollection<Seller>(db.Sellers.Where(i => i.Patronymic == SellerPatronymic).Include(p => p.Position));
                                        break;
                                    case FilterAction.ByPosition:
                                        Sellers = new ObservableCollection<Seller>(db.Sellers.Where(i => i.Position.Title == SellerPosition).Include(p => p.Position));
                                        break;
                                }
                                break;
                            case Tables.Warehouses:
                                switch (filterAction)
                                {
                                    case FilterAction.ByAddress:
                                        Warehouses = new ObservableCollection<Warehouse>(db.Warehouses.Where(i => i.Address == WarehouseAddress));
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }));
            }
        }
        #endregion

    }
}

