using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTB_Hakaton.Models.Requests.Wallet
{
    public class TransferNftRequestModel
    {
        public string Email { get; set; }
        public int NftId { get; set; }
    }
}
