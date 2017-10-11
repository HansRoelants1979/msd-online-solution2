﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeXrmEasy;
using Tc.Crm.Service.Services;
using Tc.Crm.ServiceTests;
using System.Net;
using System.Web.Http.Hosting;
using System.Web.Http;
using Tc.Crm.Service.Models;
using System.Collections.ObjectModel;

namespace Tc.Crm.Service.Controllers.Tests
{
    [TestClass()]
    public class BookingControllerTests
    {
        XrmFakedContext context;
        IBookingService bookingService;
        BookingController controller;
        ICrmService crmService;
        Booking bookingWithNmber;

        [TestInitialize()]
        public void TestSetup()
        {
            context = new XrmFakedContext();

            crmService = new TestCrmService(context);
            bookingService = new BookingService(new CacheBuckets.BrandBucket(crmService)
                                                , new CacheBuckets.CountryBucket(crmService)
                                                , new CacheBuckets.CurrencyBucket(crmService)
                                                , new CacheBuckets.GatewayBucket(crmService)
                                                , new CacheBuckets.SourceMarketBucket(crmService)
                                                , new CacheBuckets.TourOperatorBucket(crmService)
                                                , new CacheBuckets.HotelBucket(crmService));
            controller = new BookingController(bookingService, crmService);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            bookingWithNmber = new Booking
            {
                BookingIdentifier = new BookingIdentifier
                {
                    BookingNumber = "1234",
                    SourceMarket = "DE",
                    BookingSystem = BookingSystem.Nurvis
                },
                BookingGeneral = new BookingGeneral
                {
                    ToCode = "TO001",
                    Destination = "DE"
                },
                Services = new BookingServices
                {
                    Accommodation = new Accommodation[]
                    {
                        new Accommodation {GroupAccommodationCode = "hot001" }
                    },
                    Transfer = new Transfer[]
                    {
                        new Transfer {ArrivalAirport="HGR",DepartureAirport="SGP" }
                    },
                    Transport = new Transport[]
                    {
                        new Transport { ArrivalAirport="HGR",DepartureAirport="SGP" }
                    }
                }
            };
        }

        [TestMethod()]
        public void BookingIsNull()
        {
            var response = controller.Update(null);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(((System.Net.Http.ObjectContent)response.Content).Value, Constants.Messages.BookingDataPassedIsNullOrCouldNotBeParsed);
        }

        [TestMethod()]
        public void BookingIdentifierIsNull()
        {
            BookingInformation bookingInfo = new BookingInformation();
            Booking booking = new Booking();
            bookingInfo.Booking = booking;
            var response = controller.Update(bookingInfo);
            Collection<string> messages = new Collection<string>();
            messages.Add(Constants.Messages.BookingNumberNotPresent);
            messages.Add(Constants.Messages.SourceKeyNotPresent);
            messages.Add(Constants.Messages.BookingSystemIsUnknown);
            messages.Add(Constants.Messages.SourceMarketMissing);
            var expectedMessage = bookingService.GetStringFrom(messages);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(expectedMessage, ((System.Net.Http.ObjectContent)response.Content).Value);
        }

        [TestMethod()]
        public void BookingNumberIsNull()
        {
            BookingInformation bookingInfo = new BookingInformation();
            Booking booking = new Booking();
            bookingInfo.Booking = booking;
            booking.BookingIdentifier = new BookingIdentifier();
            booking.BookingIdentifier.BookingSystem = BookingSystem.Nurvis;
            booking.BookingIdentifier.SourceMarket = "DE";
            booking.BookingIdentifier.SourceSystemId = SourceSystemId.OnTour;
            var response = controller.Update(bookingInfo);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(((System.Net.Http.ObjectContent)response.Content).Value, Constants.Messages.BookingNumberNotPresent);
        }

        [TestMethod()]
        public void BookingDatabaseNotPresentCheck()
        {
            BookingInformation bookingInfo = new BookingInformation();
            Booking booking = new Booking();
            bookingInfo.Booking = booking;
            booking.BookingIdentifier = new BookingIdentifier();
            booking.BookingIdentifier.BookingSystem = BookingSystem.Nurvis;
            booking.BookingIdentifier.SourceMarket = "DE";
            booking.BookingIdentifier.BookingNumber = "123423";
            booking.BookingIdentifier.SourceSystemId = SourceSystemId.Unknown;
            var response = controller.Update(bookingInfo);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(Constants.Messages.BookingDatabaseNotPresent,((System.Net.Http.ObjectContent)response.Content).Value);
        }

        [TestMethod()]
        public void BookingConsultationReferenceNotPresentForTCVCheck()
        {
            BookingInformation bookingInfo = new BookingInformation();
            Booking booking = new Booking();
            bookingInfo.Booking = booking;
            booking.BookingIdentifier = new BookingIdentifier();
            booking.BookingIdentifier.BookingSystem = BookingSystem.Nurvis;
            booking.BookingIdentifier.SourceMarket = "DE";
            booking.BookingIdentifier.BookingNumber = "123423";
            booking.BookingIdentifier.SourceSystemId = SourceSystemId.TCV;
            var response = controller.Update(bookingInfo);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(Constants.Messages.ConsultationReferenceNotPresent, ((System.Net.Http.ObjectContent)response.Content).Value);
        }

        [TestMethod()]
        public void CustomerSourceMarketMissingCheck()
        {
            BookingInformation bookingInfo = new BookingInformation();
            Booking booking = new Booking();
            bookingInfo.Booking = booking;
            booking.BookingIdentifier = new BookingIdentifier();
            booking.BookingIdentifier.BookingSystem = BookingSystem.Nurvis;
            booking.BookingIdentifier.SourceMarket = "DE";
            booking.BookingIdentifier.BookingNumber = "123423";
            booking.BookingIdentifier.SourceSystemId = SourceSystemId.OnTour;
            booking.Customer = new Customer();
            booking.Customer.CustomerIdentifier = new CustomerIdentifier();
            booking.Customer.CustomerIdentifier.CustomerId = "123";
            var response = controller.Update(bookingInfo);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(Constants.Messages.CustomerSourceMarketMissing, ((System.Net.Http.ObjectContent)response.Content).Value);
        }

        //[TestMethod()]
        //public void SourceMarketIsNotPresentinCrm()
        //{
        //    BookingInformation bookingInfo = new BookingInformation();
        //    Booking booking = new Booking();
        //    bookingInfo.Booking = booking;
        //    booking.BookingIdentifier = new BookingIdentifier();
        //    booking.BookingIdentifier.BookingSystem = BookingSystem.Nurvis;
        //    booking.BookingIdentifier.BookingNumber = "1234";
        //    booking.BookingIdentifier.SourceMarket = "XX";
        //    var response = controller.Update(bookingInfo);
        //    Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        //    Assert.AreEqual(((System.Net.Http.ObjectContent)response.Content).Value, Constants.Messages.SourceMarketMissing);
        //}

        //[TestMethod()]
        //public void BookingCreated()
        //{
        //    BookingInformation bookingInfo = new BookingInformation();
        //    bookingInfo.Booking = bookingWithNmber;
        //    TestCrmService service = new TestCrmService(context);
        //    service.Switch = DataSwitch.Created;
        //    controller = new BookingController(bookingService, service);
        //    controller.Request = new System.Net.Http.HttpRequestMessage();
        //    controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        //    var response = controller.Update(bookingInfo);
        //    Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        //}

        //[TestMethod()]
        //public void BookingUpdated()
        //{
        //    BookingInformation bookingInfo = new BookingInformation();
        //    bookingInfo.Booking = bookingWithNmber;
        //    TestCrmService service = new TestCrmService(context);
        //    service.Switch = DataSwitch.Updated;
        //    controller = new BookingController(bookingService, service);
        //    controller.Request = new System.Net.Http.HttpRequestMessage();
        //    controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        //    var response = controller.Update(bookingInfo);
        //    Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        //}

        [TestMethod()]
        public void ActionResponseIsNull()
        {
            BookingInformation bookingInfo = new BookingInformation();
            bookingInfo.Booking = bookingWithNmber;
            bookingInfo.Booking.BookingIdentifier.SourceSystemId = SourceSystemId.OnTour;
            TestCrmService service = new TestCrmService(context);
            service.Switch = DataSwitch.Response_NULL;
            controller = new BookingController(bookingService, service);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var response = controller.Update(bookingInfo);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
        }



        [TestMethod()]
        public void ActionThrowsException()
        {
            BookingInformation bookingInfo = new BookingInformation();
            bookingInfo.Booking = bookingWithNmber;
            bookingInfo.Booking.BookingIdentifier.SourceSystemId = SourceSystemId.OnTour;
            TestCrmService service = new TestCrmService(context);
            service.Switch = DataSwitch.ActionThrowsError;
            controller = new BookingController(bookingService, service);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var response = controller.Update(bookingInfo);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
        }


    }
}