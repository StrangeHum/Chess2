using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Chess2.Models
{
    public class King : Piece
    {
        public override string Symbol =>
            IsWhite ? "♔" : "♚";
    }
}
