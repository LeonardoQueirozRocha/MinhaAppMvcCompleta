﻿using Microsoft.AspNetCore.Mvc.Razor;

namespace DevIO.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormataDocumento(this RazorPage page, int tipoPessoa, string documento)
        {
            return tipoPessoa switch
            {
                1 => Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00"),
                2 => Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00"),
                _ => throw new NotImplementedException()
            };
        }
    }
}
