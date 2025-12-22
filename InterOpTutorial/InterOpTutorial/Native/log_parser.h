#ifndef LOG_PARSER_H
#define LOG_PARSER_H

#include <stdint.h>
#include <stddef.h>

#ifdef __cplusplus
extern "C" {
#endif

// Structure to hold level counts
typedef struct
{
    int32_t error;
    int32_t warn;
    int32_t info;
    int32_t debug;
    int32_t unknown;
} LevelCounts;

/**
 * Count all log levels in a batch
 * 
 * @param buffer Concatenated buffer containing all log lines
 * @param line_offsets Array of offsets for each line in buffer
 * @param line_lengths Array of lengths for each line
 * @param line_count Number of lines
 * @param out_counts Output structure for counts
 * @return 0 on success, -1 on error
 */
int32_t count_by_level_batch(
    const uint8_t* buffer,
    const uint32_t* line_offsets,
    const uint32_t* line_lengths,
    size_t line_count,
    LevelCounts* out_counts
);

#ifdef __cplusplus
}
#endif

#endif // LOG_PARSER_H
