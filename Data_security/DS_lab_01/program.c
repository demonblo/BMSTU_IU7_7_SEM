                
#include <stdio.h>                   
#include <string.h>                 
extern int start();                  
int main()                       
{                          
    FILE *f; 
    char buff[13]; 
    if ((f = popen("ioreg -l | awk -F\\\" '/IOPlatformSerialNumber/ {print $4;}'", "r")) == NULL) {
        printf("Don't work on your machine!");      
        return -1;                        
    }                          
    fgets(buff, sizeof(buff), f);          
    fclose(f);  
    if (!strcmp(buff, "C02WD0SNHV2R")){ 
        printf("Hello!"); 
    }              
    else {   
        printf("Access is denied");
    }        
}                          
