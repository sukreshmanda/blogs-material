#include "log_parser.h"
#include <string.h>

// cc -dynamiclib -o log_parser_native.dylib log_parser.c
// Log level enum (internal use)
typedef enum
{
    LOG_LEVEL_ERROR = 0,
    LOG_LEVEL_WARN = 1,
    LOG_LEVEL_INFO = 2,
    LOG_LEVEL_DEBUG = 3,
    LOG_LEVEL_UNKNOWN = 255
} LogLevel;

// Fast inline function to parse log level from first 5 bytes
static inline LogLevel parse_level_fast(const uint8_t* data, size_t len)
{
    if (len < 5)
    {
        return LOG_LEVEL_UNKNOWN;
    }

    // Check ERROR
    if (data[0] == 'E' && data[1] == 'R' && data[2] == 'R' &&
        data[3] == 'O' && data[4] == 'R')
    {
        return LOG_LEVEL_ERROR;
    }

    // Check WARN_
    if (data[0] == 'W' && data[1] == 'A' && data[2] == 'R' &&
        data[3] == 'N' && data[4] == '_')
    {
        return LOG_LEVEL_WARN;
    }

    // Check INFO_
    if (data[0] == 'I' && data[1] == 'N' && data[2] == 'F' &&
        data[3] == 'O' && data[4] == '_')
    {
        return LOG_LEVEL_INFO;
    }

    // Check DEBUG
    if (data[0] == 'D' && data[1] == 'E' && data[2] == 'B' &&
        data[3] == 'U' && data[4] == 'G')
    {
        return LOG_LEVEL_DEBUG;
    }

    return LOG_LEVEL_UNKNOWN;
}

int32_t count_by_level_batch(
    const uint8_t* buffer,
    const uint32_t* line_offsets,
    const uint32_t* line_lengths,
    size_t line_count,
    LevelCounts* out_counts
)
{
    // Validate inputs
    if (buffer == NULL || line_offsets == NULL ||
        line_lengths == NULL || out_counts == NULL)
    {
        return -1;
    }

    // Initialize counts to zero
    out_counts->error = 0;
    out_counts->warn = 0;
    out_counts->info = 0;
    out_counts->debug = 0;
    out_counts->unknown = 0;

    // Process each log line
    for (size_t i = 0; i < line_count; i++)
    {
        uint32_t offset = line_offsets[i];
        uint32_t len = line_lengths[i];

        const uint8_t* line = buffer + offset;
        LogLevel level = parse_level_fast(line, len);

        // Increment appropriate counter
        switch (level)
        {
        case LOG_LEVEL_ERROR:
            out_counts->error++;
            break;
        case LOG_LEVEL_WARN:
            out_counts->warn++;
            break;
        case LOG_LEVEL_INFO:
            out_counts->info++;
            break;
        case LOG_LEVEL_DEBUG:
            out_counts->debug++;
            break;
        default:
            out_counts->unknown++;
            break;
        }
    }

    return 0;
}
