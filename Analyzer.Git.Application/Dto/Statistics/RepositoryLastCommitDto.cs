﻿using System;
using LibGit2Sharp;

namespace GitAnalyzer.Application.Dto.Statistics
{
    /// <summary>
    /// DTO последнего коммита в репозитории
    /// </summary>
    public class RepositoryLastCommitDto
    {
        /// <summary>
        /// Имя репозитория
        /// </summary>
        public string RepositoryName { get; set; }

        /// <summary>
        /// Хеш последнего коммита из репозитория
        /// </summary>
        public string RepositoryHash { get; set; }

        /// <summary>
        /// Дата последнего коммита из репозитория
        /// </summary>
        public DateTimeOffset? RepositoryDate { get; set; }
    }
}
