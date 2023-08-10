﻿using HomebankingMindHub.Models;
using System;
using System.Linq;

public class Transaction

{
    public long  Id { get; set; }

    public string Type { get; set; }

    public double Amount { get; set; }

    public string Description { get; set; }

    public DateTime Date { get; set; }

    public long AccountId { get; set; }

    public Account Account { get; set; }

}