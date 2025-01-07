# Ija Orisha: Battle of the Gods

## Idea

*Ija Orisha* (translated as *Battle of the Gods*) began as an evolution of our dance game, *DancePal*, originally conceived for an AWS challenge. During development, we pivoted to a card-based game with one central goal: to showcase the vibrant mythology of African deities to a global audience.

## Story

In the ancient and prosperous kingdom of **Ilu Ori**, nestled in the remote regions of West Africa, life was bountiful under the reign of the mighty King **Oba Oladori**. Known for his wisdom, strength, and countless wives, Oba Oladori fathered so many children that even he lost count. For 100 years, Ilu Ori flourished under his rule, but everything changed when the king succumbed to a mysterious illness.  

With Oba Oladori gone, the kingdom plunged into chaos. The kingmakers, tasked with selecting the next ruler, faced an impossible dilemma every son and daughter of Oladori claimed equal right to the throne. Unable to resolve the conflict through deliberation, they devised a dramatic solution: **a duel to determine the strongest heir**.

Each child of Oladori inherited the ability to summon the gods of their father using sacred cards imbued with divine power. Armed with these cards, the children must now battle each other in an epic contest of strength and strategy. Only the victor, the one who proves themselves worthy in combat, will ascend to the throne.  

This is where the story of *Ija Orisha* begins—a thrilling battle of gods, wits, and wills.  

## Challenges  

Developing *Ija Orisha* came with unique technical and resource challenges, especially as we transitioned to AWS for the hackathon:  

1. **Server Infrastructure**  
   We needed a server to handle player sessions that was cost-effective and easy to manage. Initially, our game programmer, Ayo, explored AWS GameLift due to its robust features like lobby management and matchmaking. However, we found it too complex for our current needs.  

2. **Custom Solution**  
   With no prior AWS experience, we decided to create a lightweight server using Node.js and deploy it via AWS Elastic Beanstalk. Although setting it up and troubleshooting without live logs was stressful, the deployment mirrored local behavior perfectly, giving us confidence in our approach.  

3. **Workarounds for AWS Tools**  
   To meet our MVP deadline, we made compromises:
   - **Player IDs**: We plan to integrate AWS Cognito but currently assign random IDs.
   - **Leaderboard & Scores**: AWS NoSQL databases are our goal, but for now, we're using temporary local storage.  
   - **GameLift**: Once we launch, we aim to revisit GameLift to refine player sessions.  

Our motto during this phase was simple: "Fake it till we make it."  

## Amazon Q  

A massive thanks to AWS for introducing **Amazon Q**, a tool that feels tailor-made for game developers. It seamlessly integrates with Rider, helping us predict and solve development problems with precision. Early errors aside, the tool's improvements saved us countless hours by allowing us to test code blocks without committing, enabling rapid prototyping.  

Without Amazon Q, our development timeline would have significantly stretched. It's now an essential part of our workflow.  

## About Us  

We are **HFrogames**, an indie studio from Lagos, Nigeria, driven by passion and self-taught expertise. Our team:  

- **Ayo**: CTO and jack-of-all-trades, handling everything from cloud infrastructure to game programming.  
- **Saheed**: Graphics generalist, responsible for creating and managing stunning game art.  
- **Mosope**: Project manager and story director, crafting compelling narratives while managing marketing, monetization, and deadlines.  

Together, we are determined to bring African stories to the forefront of the gaming world. *Ija Orisha* is just the beginning.  
