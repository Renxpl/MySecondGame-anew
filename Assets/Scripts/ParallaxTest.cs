using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTest : MonoBehaviour
{

    //My parallax system depends on camera displacement, cameraCurrentPosition - cameraStartingPosition.
    //If I want to parallax something then I just add cameraDisplacement to parallax object's starting
    //position considering its speed. Here I defined and assigned necessary things for the rest.
    public Transform[] groundsTransforms;
    Vector2[] parallaxStartingPositions = new Vector2[3];
    Vector2[] targetsForGround = new Vector2[3];
    int counter;

    public Transform sunTransform;
    Vector2 sunStartingPosition;
    Vector2 targetForSun;

    public Transform[] cloudTransforms;
    Vector2[] parallaxStartingPositionsForSky = new Vector2[3];
    Vector2[] targetsForSky = new Vector2[3];
    int counterForSky;

    // public Transform[] backMountainTransforms;
    // Vector2[] parallaxStartingPositionsForBM = new Vector2[3];
    // Vector2[] targetsForBM = new Vector2[3];
    // int counterForBM;

    Vector2 cameraStartingPosition;
    Vector2 cameraTotalDisplacement;

    //Assigned parallax's objects starting position to their startingPosition variables.
    void Start()
    {
        if (GameEvents.gameEvents != null) GameEvents.gameEvents.onUpdateParallax += MovingParallax;
        cameraStartingPosition = transform.position;

        for (int i = 0; i < 3; i += 2)
        {
            groundsTransforms[i] = Instantiate(groundsTransforms[1]);
            groundsTransforms[i].position = new Vector2(groundsTransforms[1].position.x - 24f + (i * 24f), groundsTransforms[1].position.y);

            cloudTransforms[i] = Instantiate(cloudTransforms[1]);
            cloudTransforms[i].position = new Vector2(cloudTransforms[1].position.x - 24f + (i * 24f), cloudTransforms[1].position.y);

            // backMountainTransforms[i] = Instantiate(backMountainTransforms[1]);
            // backMountainTransforms[i].position = new Vector2(backMountainTransforms[1].position.x - 16f + (i * 16f), backMountainTransforms[1].position.y);
        }

        for (int i = 0; i < groundsTransforms.Length; i++)
        {
            parallaxStartingPositions[i] = groundsTransforms[i].position;
            parallaxStartingPositionsForSky[i] = cloudTransforms[i].position;
            // parallaxStartingPositionsForBM[i] = backMountainTransforms[i].position;
        }

        sunStartingPosition = sunTransform.position;
    }

    //Calculating camareDisplacement, and moving parallax objects and creating them if necessary by methods.
    //I used lateupdate to ensure calculations will be done properly.
    void LateUpdate()
    {
        /*
        cameraTotalDisplacement = (Vector2)transform.position - cameraStartingPosition;

        ReproductionOfParallax(cloudTransforms, parallaxStartingPositionsForSky, targetsForSky, 24f, ref counterForSky, 9 / 10f, cameraTotalDisplacement);
        ReproductionOfParallax(groundsTransforms, parallaxStartingPositions, targetsForGround, 24f, ref counter, 1 / 2f, cameraTotalDisplacement);
        // ReproductionOfParallax(backMountainTransforms, parallaxStartingPositionsForBM, targetsForBM, 16f, ref counterForBM, 7f / 10f, cameraTotalDisplacement);

        ParallaxTransform(sunTransform, sunStartingPosition, ref targetForSun, cameraTotalDisplacement, 1f, 0f, 20f);
        ParallaxTransform(cloudTransforms, parallaxStartingPositionsForSky, ref targetsForSky, cameraTotalDisplacement, 9f / 10f, 0f, 12f);
        ParallaxTransform(groundsTransforms, parallaxStartingPositions, ref targetsForGround, cameraTotalDisplacement, 7f / 10f, 0f, 20f, true);
        //ParallaxTransform(groundsTransforms, parallaxStartingPositions,ref targetsForGround, cameraTotalDisplacement, 1f / 2f, 1 / 128f, 25f, true);
        //ParallaxTransform(backMountainTransforms, parallaxStartingPositionsForBM,ref targetsForBM, cameraTotalDisplacement, 7f / 10f, 1 / 64f, 20f, true);
       */

    }


    void MovingParallax()
    {

        cameraTotalDisplacement = (Vector2)transform.position - cameraStartingPosition;

        ReproductionOfParallax(cloudTransforms, parallaxStartingPositionsForSky, targetsForSky, 24f, ref counterForSky, 9 / 10f, cameraTotalDisplacement);
        ReproductionOfParallax(groundsTransforms, parallaxStartingPositions, targetsForGround, 24f, ref counter, 1 / 2f, cameraTotalDisplacement);
        // ReproductionOfParallax(backMountainTransforms, parallaxStartingPositionsForBM, targetsForBM, 16f, ref counterForBM, 7f / 10f, cameraTotalDisplacement);

        ParallaxTransform(sunTransform, sunStartingPosition, ref targetForSun, cameraTotalDisplacement, 1f, 0f, 20f);
        ParallaxTransform(cloudTransforms, parallaxStartingPositionsForSky, ref targetsForSky, cameraTotalDisplacement, 9f / 10f, 0f, 12f);
        ParallaxTransform(groundsTransforms, parallaxStartingPositions, ref targetsForGround, cameraTotalDisplacement, 7f / 10f, 0f, 20f, true);
        //ParallaxTransform(groundsTransforms, parallaxStartingPositions,ref targetsForGround, cameraTotalDisplacement, 1f / 2f, 1 / 128f, 25f, true);
        //ParallaxTransform(backMountainTransforms, parallaxStartingPositionsForBM,ref targetsForBM, cameraTotalDisplacement, 7f / 10f, 1 / 64f, 20f, true);
    }

    //Simply moving parallax objects, there are 2 overloaded methods, one for arrays(reproducted parallaxas)
    //the other for single objects which are moving at the same speed of camera. 
    void ParallaxTransform(Transform[] parallax, Vector2[] startingPositions, ref Vector2[] targets, Vector2 cameraDisplacement, float speed, float pixelLimit, float lerpFactor)
    {

        for (int i = 0; i < 3; i++)
        {
            targets[i] = startingPositions[i] + (cameraDisplacement * speed);
        }

        if (Mathf.Abs(targets[2].x - parallax[2].position.x) > pixelLimit || Mathf.Abs(targets[2].y - parallax[2].position.y) > pixelLimit)
        {

            Vector2 lerpResult2 = Vector2.Lerp(parallax[2].position, targets[2], Time.deltaTime * lerpFactor);
            Vector2 lerpResult0 = Vector2.Lerp(parallax[0].position, targets[0], Time.deltaTime * lerpFactor);
            Vector2 lerpResult1 = Vector2.Lerp(parallax[1].position, targets[1], Time.deltaTime * lerpFactor);

            Vector2 lerpResult2y = Vector2.Lerp(parallax[2].position, targets[2], 1);
            Vector2 lerpResult0y = Vector2.Lerp(parallax[0].position, targets[0], 1);
            Vector2 lerpResult1y = Vector2.Lerp(parallax[1].position, targets[1], 1);

            lerpResult2 = new Vector2(lerpResult2.x, lerpResult2y.y);
            lerpResult0 = new Vector2(lerpResult0.x, lerpResult0y.y);
            lerpResult1 = new Vector2(lerpResult1.x, lerpResult1y.y);

            /*
            parallax[2].position = lerpResult2;
            parallax[0].position = lerpResult0;
            parallax[1].position = lerpResult1;
            */
            parallax[2].position = new Vector2(targets[2].x, parallax[2].position.y);
            parallax[0].position = new Vector2(targets[0].x, parallax[0].position.y);
            parallax[1].position = new Vector2(targets[1].x, parallax[1].position.y);

           

        }

    }

    void ParallaxTransform(Transform[] parallax, Vector2[] startingPositions, ref Vector2[] targets, Vector2 cameraDisplacement, float speed, float pixelLimit, float lerpFactor, bool isGround)
    {

        for (int i = 0; i < 3; i++)
        {
            targets[i] = startingPositions[i] + (cameraDisplacement * speed);
        }

        if (Mathf.Abs(targets[2].x - parallax[2].position.x) > pixelLimit || Mathf.Abs(targets[2].y - parallax[2].position.y) > pixelLimit)
        {

            Vector2 lerpResult2 = Vector2.Lerp(parallax[2].position, targets[2], Time.deltaTime * lerpFactor);
            Vector2 lerpResult0 = Vector2.Lerp(parallax[0].position, targets[0], Time.deltaTime * lerpFactor);
            Vector2 lerpResult1 = Vector2.Lerp(parallax[1].position, targets[1], Time.deltaTime * lerpFactor);

            //Vector2 lerpResult2y = Vector2.Lerp(parallax[2].position, targets[2], 1);
            //Vector2 lerpResult0y = Vector2.Lerp(parallax[0].position, targets[0], 1);
            //Vector2 lerpResult1y = Vector2.Lerp(parallax[1].position, targets[1], 1);

            //lerpResult2 = new Vector2(lerpResult2.x, lerpResult2y.y);
            //lerpResult0 = new Vector2(lerpResult0.x, lerpResult0y.y);
            //lerpResult1 = new Vector2(lerpResult1.x, lerpResult1y.y);

            /*
            parallax[2].position = lerpResult2;
            parallax[0].position = lerpResult0;
            parallax[1].position = lerpResult1;
            */
            parallax[2].position = new Vector2(targets[2].x, parallax[2].position.y);
            parallax[0].position = new Vector2(targets[0].x, parallax[0].position.y);
            parallax[1].position = new Vector2(targets[1].x, parallax[1].position.y);



        }

    }

    void ParallaxTransform(Transform parallax, Vector2 startingPosition, ref Vector2 target, Vector2 cameraDisplacement, float speed, float pixelLimit, float lerpFactor)
    {

        target = startingPosition + (cameraDisplacement * speed);

        if (Mathf.Abs(target.x - parallax.position.x) > pixelLimit || Mathf.Abs(target.y - parallax.position.y) > pixelLimit)
        {
            Vector2 lerpResult = Vector2.Lerp(parallax.position, target, Time.deltaTime * lerpFactor);
            Vector2 lerpResult2 = Vector2.Lerp(parallax.position, target, 1);
            lerpResult = new Vector2(lerpResult.x, lerpResult2.y);
            //parallax.position = lerpResult;
            //parallax.position = new Vector2((float)Rounding(lerpResult.x, target.x - parallax.position.x), (float)Rounding(lerpResult.y, target.y - parallax.position.y));
            parallax.position = new Vector2(target.x, parallax.position.y);
        }

    }

    //If a parallax background isn't at the same speed of camera, then it needs to be produced
    //and placed accordingly to make it continuous
    void ReproductionOfParallax(Transform[] parallax, Vector2[] startingPositions, Vector2[] targets, float width, ref int counters, float speed, Vector2 cameraDisplacement)
    {
        if (counters == 0) { counters += 3; }

        int leftOne = counters % 3;
        int middleOne = (counters + 1) % 3;
        int rightOne = (counters + 2) % 3;



        if (this.transform.position.x > (parallax[middleOne].position.x + (width / 2f)))
        {
            startingPositions[leftOne].x += (3 * width);

            Destroy(parallax[leftOne].gameObject);
            parallax[leftOne] = Instantiate(parallax[rightOne]);
            parallax[leftOne].position = new Vector2(parallax[rightOne].position.x + width, parallax[rightOne].position.y);

            targets[leftOne] = startingPositions[leftOne] + (cameraDisplacement * speed);

            counters++;

        }


        else if (this.transform.position.x < (parallax[middleOne].position.x - (width / 2f)))
        {
            startingPositions[rightOne].x -= (3 * width);

            Destroy(parallax[rightOne].gameObject);
            parallax[rightOne] = Instantiate(parallax[leftOne]);
            parallax[rightOne].position = new Vector2(parallax[leftOne].position.x - width, parallax[leftOne].position.y);

            targets[rightOne] = startingPositions[rightOne] + (cameraDisplacement * speed);

            counters--;
        }


    }

    //Sun which is moving at the same speed of camera was too jittery even for 16 pixels/unit parallax,
    //so I rounded numbers of its movement to smooth it
    float Rounding(float number, float direction)
    {
        number *= 120f;
        int holding = (int)number;
        number -= holding;

        number = RoundingAuxiliary(number, direction);

        number += holding;
        number /= 120f;

        return number;
    }

    float RoundingAuxiliary(float number, float direction)
    {


        float sign = Mathf.Sign(number);
        number = Mathf.Abs(number);

        if (direction > 0)
        {
            if (number < 0.1) { number = 0.1f; }
            else if (number < 0.2) { number = 0.2f; }
            else if (number < 0.3) { number = 0.3f; }
            else if (number < 0.4) { number = 0.4f; }
            else if (number < 0.5) { number = 0.5f; }
            else if (number < 0.6) { number = 0.6f; }
            else if (number < 0.7) { number = 0.7f; }
            else if (number < 0.8) { number = 0.8f; }
            else if (number < 0.9) { number = 0.9f; }
            else { number = 1f; }
        }
        else if (direction < 0)
        {
            if (number < 0.1) { number = 0f; }
            else if (number < 0.2) { number = 0.1f; }
            else if (number < 0.3) { number = 0.2f; }
            else if (number < 0.4) { number = 0.3f; }
            else if (number < 0.5) { number = 0.4f; }
            else if (number < 0.6) { number = 0.5f; }
            else if (number < 0.7) { number = 0.6f; }
            else if (number < 0.8) { number = 0.7f; }
            else if (number < 0.9) { number = 0.8f; }
            else { number = 0.9f; }

        }



        number *= sign;
        return number;
    }

}
