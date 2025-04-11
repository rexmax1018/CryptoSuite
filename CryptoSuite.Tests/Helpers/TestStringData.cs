﻿namespace CryptoSuite.Tests.Helpers;

public static class TestStringData
{
    public static IEnumerable<object[]> NullOrWhitespaceStrings()
    {
        yield return new object[] { null! };
        yield return new object[] { "" };
        yield return new object[] { "   " };
    }
}