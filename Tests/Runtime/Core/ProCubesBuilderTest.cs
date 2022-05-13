using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PCB.Core;

public class ProCubesBuilderTest
{
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        var builder = new ProCubesBuilder();

        builder.QueueCube(new ProCubeInstance(CubeSide.Top, 10, 2));

        builder.Build();

        yield return null;
    }
}
