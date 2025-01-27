using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Cargo.EntityLayer.Concrete
{
    public class CargoDetail
    {
        public int CargoDetailId { get; set; }
        public string SenderCustomer { get; set; }
        public string ReceiverCustomer { get; set; } //mongoDB'den alacagiz, o yüzden string yaptim.
        public int Barcode { get; set; }

        //relationship
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }


    }
}
