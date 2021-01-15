package org.salem.common;

import java.util.ArrayList;

public final class Casing
{
    public static String getCamelCaseWords(String camelCaseString)
    {
        if (camelCaseString == null)
            throw new IllegalArgumentException("The string must not be null.");

        if (camelCaseString.length() == 0)
            return camelCaseString;

        var indexList = new ArrayList<Integer>()
        {{
            add(0);
        }};

        for (int i = 1; i < camelCaseString.length(); i++)
            if (Character.isUpperCase(camelCaseString.charAt(i)))
                indexList.add(i);

        indexList.add(camelCaseString.length());
        var result = new StringBuilder();

        for (int i = 0; i < indexList.size() - 1; i++)
            result.append(camelCaseString, indexList.get(i), indexList.get(i + 1)).append(' ');

        return result.deleteCharAt(result.length() - 1).toString();
    }
}
