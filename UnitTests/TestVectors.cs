﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace UnitTests
{
    [TestClass]
    public class TestVectors
    {
        [TestMethod]
        public void it_matches_all_tests_vectors()
        {
            UInt64[,] testVectors =
            {
                { 0x0000000000000000, 0x0000000000000000, 0x4EF997456198DD78 },
                { 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, 0x51866FD5B85ECB8A },
                { 0x3000000000000000, 0x1000000000000001, 0x7D856F9A613063F2 },
                { 0x1111111111111111, 0x1111111111111111, 0x2466DD878B963C9D },
                { 0x0123456789ABCDEF, 0x1111111111111111, 0x61F9C3802281B096 },
                { 0x1111111111111111, 0x0123456789ABCDEF, 0x7D0CC630AFDA1EC7 },
                { 0x0000000000000000, 0x0000000000000000, 0x4EF997456198DD78 },
                { 0xFEDCBA9876543210, 0x0123456789ABCDEF, 0x0ACEAB0FC6A0A28D },
                { 0x7CA110454A1A6E57, 0x01A1D6D039776742, 0x59C68245EB05282B },
                { 0x0131D9619DC1376E, 0x5CD54CA83DEF57DA, 0xB1B8CC0B250F09A0 },
                { 0x07A1133E4A0B2686, 0x0248D43806F67172, 0x1730E5778BEA1DA4 },
                { 0x3849674C2602319E, 0x51454B582DDF440A, 0xA25E7856CF2651EB },
                { 0x04B915BA43FEB5B6, 0x42FD443059577FA2, 0x353882B109CE8F1A },
                { 0x0113B970FD34F2CE, 0x059B5E0851CF143A, 0x48F4D0884C379918 },
                { 0x0170F175468FB5E6, 0x0756D8E0774761D2, 0x432193B78951FC98 },
                { 0x43297FAD38E373FE, 0x762514B829BF486A, 0x13F04154D69D1AE5 },
                { 0x07A7137045DA2A16, 0x3BDD119049372802, 0x2EEDDA93FFD39C79 },
                { 0x04689104C2FD3B2F, 0x26955F6835AF609A, 0xD887E0393C2DA6E3 },
                { 0x37D06BB516CB7546, 0x164D5E404F275232, 0x5F99D04F5B163969 },
                { 0x1F08260D1AC2465E, 0x6B056E18759F5CCA, 0x4A057A3B24D3977B },
                { 0x584023641ABA6176, 0x004BD6EF09176062, 0x452031C1E4FADA8E },
                { 0x025816164629B007, 0x480D39006EE762F2, 0x7555AE39F59B87BD },
                { 0x49793EBC79B3258F, 0x437540C8698F3CFA, 0x53C55F9CB49FC019 },
                { 0x4FB05E1515AB73A7, 0x072D43A077075292, 0x7A8E7BFA937E89A3 },
                { 0x49E95D6D4CA229BF, 0x02FE55778117F12A, 0xCF9C5D7A4986ADB5 },
                { 0x018310DC409B26D6, 0x1D9D5C5018F728C2, 0xD1ABB290658BC778 },
                { 0x1C587F1C13924FEF, 0x305532286D6F295A, 0x55CB3774D13EF201 },
                { 0x0101010101010101, 0x0123456789ABCDEF, 0xFA34EC4847B268B2 },
                { 0x1F1F1F1F0E0E0E0E, 0x0123456789ABCDEF, 0xA790795108EA3CAE },
                { 0xE0FEE0FEF1FEF1FE, 0x0123456789ABCDEF, 0xC39E072D9FAC631D },
                { 0x0000000000000000, 0xFFFFFFFFFFFFFFFF, 0x014933E0CDAFF6E4 },
                { 0xFFFFFFFFFFFFFFFF, 0x0000000000000000, 0xF21E9A77B71C49BC },
                { 0x0123456789ABCDEF, 0x0000000000000000, 0x245946885754369A },
                { 0xFEDCBA9876543210, 0xFFFFFFFFFFFFFFFF, 0x6B5C5A9C5D9E0A5A }
            };

            int count = testVectors.GetLength(0);

            for (int i = 0; i < count; i++)
            {
                // To test each vector, which is given above as a hexadecimal string, we'll unpack the three
                // vectors into byte arrays.
                byte[] Key = ByteOperations.UnpackUInt64IntoBytes(testVectors[i, 0]);
                byte[] Cleartext = ByteOperations.UnpackUInt64IntoBytes(testVectors[i, 1]);
                byte[] Expected = ByteOperations.UnpackUInt64IntoBytes(testVectors[i, 2]);

                AustinXYZ.BlowfishManaged blowfish = new AustinXYZ.BlowfishManaged();
                blowfish.Key = Key;
                blowfish.Mode = CipherMode.ECB;
                ICryptoTransform transform = blowfish.CreateEncryptor();
                byte[] Actual = transform.TransformFinalBlock(Cleartext, 0, 8);

                String errorMessage = String.Format("Expected: 0x{0} Actual: 0x{1}",
                    BitConverter.ToString(Expected),
                    BitConverter.ToString(Actual));

                CollectionAssert.AreEqual(Expected, Actual, errorMessage);
            }
        }
    }
}
