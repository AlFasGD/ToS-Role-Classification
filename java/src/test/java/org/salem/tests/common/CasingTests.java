package org.salem.tests.common;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.salem.common.Casing;

public class CasingTests
{
    @Test
    public void getCamelCaseWordsTest()
    {
        Assertions.assertEquals("Guardian Angel", Casing.getCamelCaseWords("GuardianAngel"));
        Assertions.assertEquals("Veteran", Casing.getCamelCaseWords("Veteran"));
    }
}
