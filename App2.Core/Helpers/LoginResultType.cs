﻿namespace App2.Core.Helpers;

public enum LoginResultType
{
    Success,
    Unauthorized,
    CancelledByUser,
    NoNetworkAvailable,
    UnknownError
}