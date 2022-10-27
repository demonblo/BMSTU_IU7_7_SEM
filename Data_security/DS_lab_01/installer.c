#include <stdio.h>
#include <unistd.h>
#include <sys/wait.h>
#include "constants.h"

void get_serial_number(char unique_id[LEN_UNIQUE_ID])
{
    FILE *f = popen("ioreg -l | awk -F\\\" '/IOPlatformSerialNumber/ {print $4;}'", "r");
    fgets(unique_id, LEN_UNIQUE_ID, f);
    fclose(f);
}

void create_c_file()
{
    FILE *j = fopen(PROGRAM_FILE_NAME, MODE_WRITE);

    char unique_id[LEN_UNIQUE_ID];
    get_serial_number(unique_id);

    fprintf(j, "%s", BEGIN_STRING);
    fprintf(j, "\\");
    fprintf(j, "\\");
    fprintf(j, "\\");
    fprintf(j, "%s", MID_STRING);
    fprintf(j, "%s", unique_id);
    //fprintf(j, "1");
    fprintf(j, "%s", END_STRING);

    fclose(j);
    execlp("gcc", "gcc", "-o", EXECUTABLE_FILE_NAME, PROGRAM_FILE_NAME, NULL);
}

int main(void)
{
    create_c_file();

    return OK;
}