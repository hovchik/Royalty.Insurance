using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.System.DocumentManagement;
using Domain;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Extensions
{
    public static class QueryableExtension
    {
        public static async Task<PaginationResponse<TResponse>> ToPaginationAsync<TResponse, TEntity>(this IOrderedQueryable<TEntity> entities,
            Expression<Func<TEntity, TResponse>> mapper,
            int pageIndex, int pageSize)
        where TResponse : class
        {
            var response = new PaginationResponse<TResponse>
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                RowCount = await entities.CountAsync()
            };

            var pageCount = (double)response.RowCount / pageSize;
            response.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (pageIndex - 1) * pageSize;

            response.Response = await entities.Skip(skip)// default
                .Take(pageSize)
                .Select(mapper)
                .ToListAsync();
            response.Response = response.Response;

            return response;
        }

        public static async Task<PaginationResponse<TResponse>> ToPaginationAsync<TResponse>(this IOrderedQueryable<TResponse> entities,
            int pageIndex, int pageSize)
            where TResponse : class
        {
            var response = new PaginationResponse<TResponse>
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                RowCount = await entities.CountAsync()
            };

            var pageCount = (double)response.RowCount / pageSize;
            response.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (pageIndex - 1) * pageSize;

            response.Response = await entities.Skip(skip)// default
                .Take(pageSize)
                .ToListAsync();
            response.Response = response.Response;

            return response;
        }

        public static async Task<Accord101FormRequest> GetAccord101FormData(this IApplicationDbContext context,
            Insured insured, string producerFullName, CancellationToken cancellationToken)
        {
            var agency = await context.Agencies.FirstOrDefaultAsync(cancellationToken);

            return new Accord101FormRequest
            {
                AgencyName = agency.Name,
                InsuredCompanyName = !string.IsNullOrEmpty(insured.Dba)
                    ? $"{insured.MailingName}:{insured.Dba}"
                    : insured.MailingName,
                InsuredCity = insured.MailingCity.Name,
                InsuredState = insured.MailingState.Name,
                InsuredZip = insured.MailingZipCode.Code,
                InsuredAddress = insured.MailingStreetAddress,
                InsuranceNameCarrier = "TODO", //figure out
                ProducerFullName = producerFullName
            };
        }

        public static async Task<Accord25FormRequest> GetAccord25FormData(this IApplicationDbContext context,
            Insured insured, string producerEmail, CancellationToken cancellationToken)
        {
            var agency = await context.Agencies.FirstOrDefaultAsync(cancellationToken);

            return new Accord25FormRequest
            {
                AgencyName = agency.Name,
                AgencyState = agency.State,
                AgencyEmail = producerEmail,
                AgencyCity = agency.City,
                AgencyZip = agency.Zip,
                AgencyPhoneNumber = agency.PhoneNumber,
                AgencyFaxNumber = agency.FaxNumber,
                AgencyAddress = agency.Address,
                InsuredCity = insured.MailingCity.Name,
                InsuredState = insured.MailingState.Name,
                InsuredZip = insured.MailingZipCode.Code,
                InsuredAddress = insured.MailingStreetAddress,
                InsuredCompanyName = !string.IsNullOrEmpty(insured.Dba) ? $"{insured.MailingName}:{insured.Dba}" : insured.MailingName,
            };
        }

        public static async Task<Accord36FormRequest> GetAccord36FormData(this IApplicationDbContext context,
            Insured insured, string producerEmail, string producerFullName, CancellationToken cancellationToken)
        {
            var agency = await context.Agencies.FirstOrDefaultAsync(cancellationToken);

            return new Accord36FormRequest
            {
                AgencyName = agency.Name,
                AgencyState = agency.State,
                AgencyEmail = producerEmail,
                ProducerFullName = producerFullName,
                AgencyCity = agency.City,
                AgencyZip = agency.Zip,
                AgencyPhoneNumber = agency.PhoneNumber,
                AgencyFaxNumber = agency.FaxNumber,
                AgencyAddress = agency.Address,
                InsuredCity = insured.MailingCity.Name,
                InsuredState = insured.MailingState.Name,
                InsuredZip = insured.MailingZipCode.Code,
                InsuredAddress = insured.MailingStreetAddress,
                InsuredCompanyName = !string.IsNullOrEmpty(insured.Dba) ? $"{insured.MailingName}:{insured.Dba}" : insured.MailingName,
            };
        }


        public static async Task<bool> IsGroupMember(this IApplicationDbContext context, int groupId, int memberId)
        {
            return await context.Groups.Where(item => item.Id.Equals(groupId) && item.GroupMembers.Any(m => m.MemberId.Equals(memberId))).AnyAsync();
        }

        public static async Task<bool> IsGroupMember(this IApplicationDbContext context, int groupId, int userRequestedId, List<int> memberIds)
        {
            var isGroupCreator = await IsGroupCreator(context, groupId, userRequestedId);
            //user is created and he is not trying to remove himself
            if (isGroupCreator && memberIds.All(item => !item.Equals(userRequestedId)))
            {
                return true;
            }

            bool isOnOfThemGroupCreator = false;
            foreach (var memberId in memberIds)
            {
                if (await IsGroupCreator(context, groupId, memberId))
                {
                    isOnOfThemGroupCreator = true;
                    break;
                }
            }
            if (isOnOfThemGroupCreator)
            {
                return false;
            }

            //if but he is a member of the group and he is not the creator
            return !isGroupCreator && await IsGroupMember(context, groupId, userRequestedId);
        }

        private static async Task<bool> IsGroupCreator(this IApplicationDbContext context, int groupId, int memberId)
        {
            return await context.Groups.Where(item => item.Id.Equals(groupId) && item.CreatedBy.Equals(memberId)).AnyAsync();
        }
    }
}
