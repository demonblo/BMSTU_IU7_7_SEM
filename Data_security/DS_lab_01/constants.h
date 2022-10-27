#define BEGIN_STRING "                \n\
#include <stdio.h>                   \n\
#include <string.h>                 \n\
extern int start();                  \n\
int main()                       \n\
{                          \n\
    FILE *f; \n\
    char buff[13]; \n\
    if ((f = popen(\"ioreg -l | awk -F"
#define MID_STRING "\" '/IOPlatformSerialNumber/ {print $4;}'\", \"r\")) == NULL) {\n\
        printf(\"Don't work on your machine!\");      \n\
        return -1;                        \n\
    }                          \n\
    fgets(buff, sizeof(buff), f);          \n\
    fclose(f);  \n\
    if (!strcmp(buff, \"\
"

#define END_STRING "\")){ \n\
        printf(\"Hello!\"); \n\
    }              \n\
    else {   \n\
        printf(\"Access is denied\");\n\
    }        \n\
}                          \n\
"


#define MODE_WRITE "w"
#define LEN_UNIQUE_ID 13
#define PROGRAM_FILE_NAME "program.c"
#define EXECUTABLE_FILE_NAME "program"
#define OK 0;