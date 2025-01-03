# Ija Orisha 

## Idea

Ija Orisha (battle of the gods) originated from the mechinic of our dance game DancePal. we were working on a dance game for aws challenge but switched to a card game. We wantend a way to tell the world about some african deaitys.

## Story

There was once and old ancient kingdom called Ilu Ori in a remote regions of west afican. This kingdom was ruled by a powerful and the kingdom was prosperous they king Oba Oladori had so many wifes and many children. He even lost count of how many children he have. He ruled over ilu ori for 100 years and everything was good until he died of an unknown illness.
The king makers were tasked with appointing a new king but because oba Oladori had so many sons and duagthers all claiming they have equal right to the throne the king makers had no choice and to invite the children to dueal and only rhe strongest of them all can be king. All the children or Oladori have access to the gods of there father they could summon any gods at well as long as they have the cards. With this the dual begins and here I introlduce to you Ija Orisha. battle of the gods

## Challenges

We want a server that could handle players session and less cumbersome to setup and less expensive too. Our game programmer Ayo had to take some aws course during the perioud of the hackathon and suggest we amazon ready made solution aws game lift. but we discorverd it might be an overkill even when it provide so much ease of life feature like lobby and session. we had no experince with any aws tools and don't know ec2 same time we using aws is essential to our product. so we built a simple socket server using node.js and deployed this on aws eleastic bean stack. Though it took some getting used to and the fact we can't see a live logs or the running application is a little streessful but luckity for us our code works same way it does locally. this alone is a huge advantage. since what we get locally is what we get on server. we are focused on release an mvp to meet up with the submission and won't include some othere aws tools we need. this is where we might fake it till we make it.
eg for player ID we will use aws coginto but for now we assign random ID to users. for score and leaderboard we are hoping to use aws no-sql database. and finally we laucnh we might give aws game lift a go. I feel a basic knowlegee about a tools is important before building with it.

## Amazon Q

First I would say thanks for aws for providing Amazon Q it feel native and seems it designed speicifically for game devs it knows my mind and it able to predict what I want to do but can't figure out. it precise and straignt forward when giving anaswer to question. Though during the first weeks got some errors but but now the tools has been greatly improved and it blends seemlessly with rider this combo is what I can't do without now. I would say without amazon q the development time would take longer. We are able to protype and test out codeblock without commiting to then just to see they would work out.

## About Us
We are Hfrogames an indie stuido of passionate  self thought induvials from Lagos Nigeria. We have Ayo the cto and the jack of all trade from cloud to backend to game programming, We have Saheed the graphics generalist responsible for mamaging our game art. Finally we have mosope the project manager and story director responsible for creating the stroy and also responsible for the marketing and monetisations also keep the team in cheak making sure we meet the deadline
