using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace DataGen.Types.Mail {
    public static class MailFactory {
        private static readonly List<string> EmailDomains = new List<string> {
            "yahoo.com", "gmail.com", "hotmail.com"
        };

        private static readonly List<string> CreatedMails = new List<string>();

        private static readonly List<char> Separators = new List<char> {
            '_', '.', '-', '!'
        };


        public static string Mail(params string[] text) {
            var attemtpts = 0;
            while (true) {
                var address =
                    $"{text.Aggregate((s, s1) => s + Separators[HelperClass.Randomizer(Separators.Count)] + s1).ToLower()}@{EmailDomains[HelperClass.Randomizer(EmailDomains.Count)]}";
                if (!CreatedMails.Contains(address)) {
                    CreatedMails.Add(address);
                    return address;
                }
                attemtpts += 1;
                if (attemtpts == 10)
                    throw new Exception("Reached maxium attempts to create mail");
            }
        }
    }
}