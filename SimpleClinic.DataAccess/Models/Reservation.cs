﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SimpleClinic.DataAccess.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public int? DoctorServiceClinicId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public bool Status { get; set; }

    public virtual DoctorServiceClinic DoctorServiceClinic { get; set; }
}