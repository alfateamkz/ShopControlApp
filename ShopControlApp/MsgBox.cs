using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopControlApp
{
    class MsgBoxCustom
    {
        public string Article { get; set; }

        public string Description { get; set; }

        
        public MsgBoxCustom(MessageCode msgCode)
        {
   
            switch (msgCode)
            {
                case MessageCode.WrongBasketQuantity:
                    if (AppConfig.JsonParameters.Language == "RU-ru")
                    {
                        this.Article = "Ошибка";
                        this.Description = "Количество товара для добавленя должно быть больше нуля";
                    }
                    else if (AppConfig.JsonParameters.Language == "EN-en")
                    {
                        this.Article = "Error";
                        this.Description = "The number of items to add must be greater than zero";
                    }
                    break;

                case MessageCode.NotEnoughAtWarehouse:
                    if (AppConfig.JsonParameters.Language == "RU-ru")
                    {
                        this.Article = "Ошибка";
                        this.Description = "Недостаточно товара на складе";
                    }
                    else if (AppConfig.JsonParameters.Language == "EN-en")
                    {
                        this.Article = "Error";
                        this.Description = "Not enough goods in stock";
                    }
                    break;
                case MessageCode.NullError:
                    if (AppConfig.JsonParameters.Language == "RU-ru")
                    {
                        this.Article = "Ошибка";
                        this.Description = "Неправильно заполненные поля";
                    }
                    else if (AppConfig.JsonParameters.Language == "EN-en")
                    {
                        this.Article = "Error";
                        this.Description = "Incorrectly filled fields";
                    }
                    break;
                case MessageCode.SuccessfulAdd:
                    if (AppConfig.JsonParameters.Language == "RU-ru")
                    {
                        this.Article = "Уведомление";
                        this.Description = "Запись успешно добавлена";
                    }
                    else if (AppConfig.JsonParameters.Language == "EN-en")
                    {
                        this.Article = "Notification";
                        this.Description = "Entry has been successfully added";
                    }
                    break;
                case MessageCode.SuccessfulDelete:
                    if (AppConfig.JsonParameters.Language == "RU-ru")
                    {
                        this.Article = "Уведомление";
                        this.Description = "Запись успешно удалена";
                    }
                    else if (AppConfig.JsonParameters.Language == "EN-en")
                    {
                        this.Article = "Notification";
                        this.Description = "Entry has been successfully deleted";
                    }
                    break;
                case MessageCode.UnsuccessfulDelete:
                    if (AppConfig.JsonParameters.Language == "RU-ru")
                    {
                        this.Article = "Ошибка";
                        this.Description = "Записи с таким ID нет";
                    }
                    else if (AppConfig.JsonParameters.Language == "EN-en")
                    {
                        this.Article = "Error";
                        this.Description = "There is no entry with this ID";
                    }
                    break;
                case MessageCode.SuccessfulUpdate:
                    if (AppConfig.JsonParameters.Language == "RU-ru")
                    {
                        this.Article = "Уведомление";
                        this.Description = "Запись успешно обновлена";
                    }
                    else if (AppConfig.JsonParameters.Language == "EN-en")
                    {
                        this.Article = "Notification";
                        this.Description = "Entry has been successfully updated";
                    }
                    break;
                case MessageCode.AuthError:
                    if (AppConfig.JsonParameters.Language == "RU-ru")
                    {
                        this.Article = "Ошибка входа";
                        this.Description = "Неверные имя пользователя или пароль";
                    }
                    else if (AppConfig.JsonParameters.Language == "EN-en")
                    {
                        this.Article = "Login failed";
                        this.Description = "Invalid username or password";
                    }
                    break;
                case MessageCode.CheckoutEnd:
                    if (AppConfig.JsonParameters.Language == "RU-ru")
                    {
                        this.Article = "Уведомление";
                        this.Description = "Покупка успешно оформлена";
                    }
                    else if (AppConfig.JsonParameters.Language == "EN-en")
                    {
                        this.Article = "Notification";
                        this.Description = "Purchase has been successfully completed";
                    }
                    break;
            }
        }

     
    
    }
    public enum MessageCode
    {
        NotEnoughAtWarehouse = 0,
        WrongBasketQuantity = 1,
        NullError = 2,
        SuccessfulUpdate = 3,
        SuccessfulAdd = 4,
        SuccessfulDelete = 5,
        AuthError = 6,
        UnsuccessfulDelete = 7,
        CheckoutEnd = 8,
        ColorError = 9
    }

 

}
