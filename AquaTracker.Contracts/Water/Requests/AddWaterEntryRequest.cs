﻿namespace AquaTracker.Contracts.Water.Requests;

public record AddWaterEntryRequest(double Amount, string Date, string Time);