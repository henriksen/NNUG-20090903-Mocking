using System;
using Moq;
using NUnit.Framework;
using SpecUnit;

namespace Mocking.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void IsPrefered_MoreThan100000InSales_True()
        {
            var customerRepository = new Mock<ICustomerRepository>();
            customerRepository
                .Setup(repository => repository.GetTotalSalesForCustomer(It.IsAny<Customer>()))
                .Returns(100001d);
            var validator = new CustomerIsPreferred(customerRepository.Object);
            validator.IsSatisfiedBy(new Customer()).ShouldBeTrue();
        }
        [Test]
        public void IsPrefered_With100000InSales_False()
        {
            var customerRepository = new Mock<ICustomerRepository>();
            customerRepository
                .Setup(repository => repository.GetTotalSalesForCustomer(It.IsAny<Customer>()))
                .Returns(100000d);
            var validator = new CustomerIsPreferred(customerRepository.Object);
            validator.IsSatisfiedBy(new Customer()).ShouldBeFalse();
        }

    }





    public class CustomerIsPreferred : ISpecification<Customer>
    {
        private readonly ICustomerRepository _repository;

        public CustomerIsPreferred(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public bool IsSatisfiedBy(Customer customer)
        {
            return _repository.GetTotalSalesForCustomer(customer) > 100000d;
            
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
    }

    public interface ICustomerRepository
    {
        double GetTotalSalesForCustomer(Customer customer);
    }

    public class Customer   
    {
    }
}