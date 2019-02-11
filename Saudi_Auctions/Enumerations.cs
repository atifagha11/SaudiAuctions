using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Saudi_Auctions
{
    public class Enumerations
    {
        public enum UserDefinedType
        { //
            [Display(Name = "MechanicalCondition")]
            MechanicalCondition,
            [Display(Name = "MechanicalCondition")]
            BodyCondition,
            [Display(Name = "TransmissionType")]
            TransmissionType, 
            [Display(Name = "NoOfCylinders")]
            NoOfCylinders,
            [Display(Name = "BodyType")]
            BodyType,
            [Display(Name = "Doors")]
            Doors,
            [Display(Name = "HorsePower")]
            HorsePower,
            [Display(Name = "Waranty")]
            Waranty,
            [Display(Name = "FuelType")]
            FuelType,
            [Display(Name = "Model")]
            Model,

        }

    }
}