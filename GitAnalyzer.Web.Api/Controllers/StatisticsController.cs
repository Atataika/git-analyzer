﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GitAnalyzer.Web.Application.Services.Statistics;
using GitAnalyzer.Web.Contracts.Statistics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GitAnalyzer.Web.Api.Controllers
{
    /// <summary>
    /// Контроллер для раьоты со статистикой GIT репозиториев
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IGitStatisticsService _gitStatisticsService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Контроллер для раьоты со статистикой GIT репозиториев
        /// </summary>
        public StatisticsController(
            IGitStatisticsService gitStatisticsService,
            IMapper mapper)
        {
            _gitStatisticsService = gitStatisticsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение статистики из GIT репозиториев 
        /// </summary>
        /// <param name="startDate">Дата начала периода в формате YYYY-MM-DD</param>
        /// <param name="endDate">Дата окончания периода в формате YYYY-MM-DD</param>
        /// <returns></returns>
        [HttpGet("{startDate}/{endDate}")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        [ProducesResponseType(typeof(IEnumerable<RepositoryStatisticsContract>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var statistics = await _gitStatisticsService.GetAllRepositoriesStatisticsAsync(startDate, endDate);

            var result = _mapper.Map<IEnumerable<RepositoryStatisticsContract>>(statistics);

            return Ok(result);
        }

        /// <summary>
        /// Обновление GIT репозиториев
        /// </summary>
        /// <returns></returns>
        [HttpGet("update-repositories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRepositories()
        {
            await _gitStatisticsService.UpdateAllRepositories();

            return NoContent();
        }

        /// <summary>
        /// Оценки затраченного на работу времени
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("work-estimates/{startDate}/{endDate}")]
        [ProducesResponseType(typeof(IEnumerable<RepositoryWorkEstimateContract>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRepositoriesWorkEstimate(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var estimates = await _gitStatisticsService.GetWorkSessionsEstimate(startDate, endDate);

            var result = _mapper.Map<IEnumerable<RepositoryWorkEstimateContract>>(estimates);

            return Ok(result);
        }
    }
}