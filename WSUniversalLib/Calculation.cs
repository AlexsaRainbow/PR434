using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSUniversalLib
{
    public class Calculation
    {
        public int GetQuantityForProduct(int productType,int materialType, int count,float width,float length)
        {
            float kofTypeProduct = 0;
            float brakMaterial = 0;
            if (productType >= 1 && productType <= 3 && (materialType == 1 || materialType == 2) && count > 0 && width > 0 && length > 0)
            {
                switch (productType)
                {
                    case 1:
                        kofTypeProduct = 1.1F;
                        break;
                    case 2:
                        kofTypeProduct = 2.5F;
                        break;
                    case 3:
                        kofTypeProduct = 8.43F;
                        break;
                }

                switch (materialType)
                {
                    case 1:
                        brakMaterial = 0.3F / 100F;
                        break;
                    case 2:
                        brakMaterial = 0.12F / 100F;
                        break;
                }
                float S = width * length;
                float kolKachSir = S * kofTypeProduct * count;
                int kolKachSirSBrak = (int)Math.Ceiling((kolKachSir * brakMaterial) + kolKachSir);
                return kolKachSirSBrak;

            }
            else
                return -1;
        }
    }
}
