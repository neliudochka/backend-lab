# Backend lab

## Variant
Number of group: IM-12  
12 % 3 = 0  
=>  
**Облік доходів**  
Облік доходів - потрібно зробити сутність “рахунок” куди можна додавати гроші по мірі їх надходження(для кожного користувача свій) і звідти списуються кошти атоматично при створенні нової витрати. Логіка щодо заходу в мінус лишається на розсуд студентів(можете або дозволити це, або заборонити).

## Installation

Clone this repository
```bash
git clone https://github.com/neliudochka/backend-lab.git
```

## Build

Move to the repo
```bash
// from the directory where you cloned repo  
cd   backend-lab
```

Now build docker images with docker-compose
 ```
 docker-compose build
``` 

## Run

and run
 ```
 docker-compose up
``` 

## Deploy
https://backend-lab-2-4uqc.onrender.com

and link especially to /healthcheck endpoint:  
https://backend-lab-2-4uqc.onrender.com/healthcheck

## Postman workspace
https://www.postman.com/orbital-module-pilot-31168210/workspace/backend-lab
