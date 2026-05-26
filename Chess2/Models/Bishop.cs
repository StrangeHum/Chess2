using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess2.Models
{
    internal class Bishop : Piece
    {
        public override string Symbol => IsWhite ? "♗" : "♝";
    }
}
