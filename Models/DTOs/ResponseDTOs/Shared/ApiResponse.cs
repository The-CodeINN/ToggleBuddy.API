﻿using Microsoft.AspNetCore.Mvc;

namespace ToggleBuddy.API.Models.DTOs.ResponseDTOs.Shared
{
    public static class ApiResponse
    {
        public static object SuccessMessage(string message)
        {
            return new
            {
                status = Status.SUCCESS,
                message
            };
        }

        public static object SuccessMessageWithData(object data)
        {
            return new
            {
                status = Status.SUCCESS,
                data
            };
        }

        public static object Failure(string status, object message)
        {
            return new
            {
                status,
                message
            };
        }

        public static object UnknownException(object message)
        {
            return new
            {
                status = Status.UNKNOWN_ERROR,
                message
            };
        }

        public static object GenericException(object message)
        {
            return new
            {
                status = Status.ERROR,
                message
            };
        }

        public static object ConflictException(object message)
        {
            return new
            {
                status = Status.CONFLICT_ERROR,
                message
            };
        }

        public static object AuthorizationException(object message)
        {
            return new
            {
                status = Status.AUTHORIZATION_ERROR,
                message
            };
        }

        public static object AuthenticationException(object message)
        {
            return new
            {
                status = Status.AUTHENTICATION_ERROR,
                message
            };
        }

        public static object NewAuthorizationException(object message)
        {
            return new ContentResult
            {
                StatusCode = 403,
                Content = "Permission Denied: You do not have permission to access this resource.",
                ContentType = "text/plain",
            };
        }
    }
}
