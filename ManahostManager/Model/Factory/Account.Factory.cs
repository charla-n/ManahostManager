using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Model.DTO.Account;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ManahostManager.Model.Factory
{
    public partial class Factory
    {
        public static ExposeAccountModel Create(Client client, IList<Claim> claims, HomeDTO defaultHome)
        {
            var dst = new ExposeAccountModel()
            {
                AcceptMailing = client.AcceptMailing,
                Civility = client.Civility,
                Country = client.Country,
                DefaultHomeId = client.DefaultHomeId,
                Email = client.Email,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Locale = client.Locale,
                InitManager = client.InitManager,
                PrincipalPhone = client.PrincipalPhone,
                SecondaryPhone = client.SecondaryPhone,
                Timezone = client.Timezone,
                TutorialManager = client.TutorialManager,
                HomeDefault = defaultHome
            };
            foreach (var claim in claims)
            {
                dst.Claims.Add(new KeyValuePair<string, string>(claim.Type, claim.Value));
            }
            return dst;
        }

        public static Client Create(CreateAccountModel model)
        {
            return new Client()
            {
                Email = model.Email,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                DateCreation = DateTime.Now,
                DateModification = null,
                DefaultHomeId = null,
                TwoFactorEnabled = false,
                InitManager = false,
                TutorialManager = false,
                Locale = "fr-FR",
                Timezone = model.Timezone,
                Country = model.Country,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Civility = model.Civility,
                IsManager = false,
                AcceptMailing = false,
                DateBirth = null
            };
        }

        public static Client Create(ParsedExternalAccessTokenUserInfo model)
        {
            return new Client()
            {
                Email = model.email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                DateCreation = DateTime.Now,
                DateModification = null,
                DefaultHomeId = null,
                TwoFactorEnabled = false,
                InitManager = false,
                TutorialManager = false,
                Locale = "fr-FR",
                Timezone = TimeZoneInfo.FindSystemTimeZoneById("UTC").BaseUtcOffset.Hours,
                Country = "",
                FirstName = model.given_name,
                LastName = model.family_name,
                Civility = "",
                IsManager = false,
                AcceptMailing = false,
                DateBirth = null
            };
        }

        public static PhoneNumber Create(PhoneModel model, int id)
        {
            return new PhoneNumber()
            {
                Id = id,
                Phone = model.Phone,
                PhoneType = model.Type
            };
        }

        public static PhoneModel Create(PhoneNumber model)
        {
            return new PhoneModel()
            {
                Phone = model.Phone,
                Type = model.PhoneType
            };
        }

        public static Client Create(Client baseModel, PutAccountModel model)
        {
            baseModel.Civility = model.Civility;
            baseModel.Country = model.Country;
            baseModel.LastName = model.LastName;
            baseModel.FirstName = model.FirstName;
            baseModel.Timezone = model.Timezone;
            return baseModel;
        }
    }
}