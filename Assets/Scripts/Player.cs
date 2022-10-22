using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text stepText;
    [SerializeField] ParticleSystem dieParticles;
    [SerializeField, Range(0.01f, 1f)] float moveDuration = 0.2f;
    [SerializeField, Range(0.01f, 1f)] float jumpHeight = 0.5f;
    private int backBoundary;
    private int leftBoundary;
    private int rightBoundary;
    [SerializeField] private int maxTravel;
    public int MaxTravel { get => maxTravel; }
    [SerializeField] private int currentTravel;
    public int CurrentTravel { get => currentTravel; }
    public bool IsDie { get => this.enabled == false; }
    public AudioSource splatDead;
    public AudioSource jumpSound;

    public void SetUp(int minZPos, int extent)
    {
        backBoundary = minZPos - 1;
        leftBoundary = -(extent + 1);
        rightBoundary = extent + 1;
    }

    private void Update()
    {
        var moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDir += new Vector3(0, 0, 1);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            moveDir += new Vector3(0, 0, -1);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            moveDir += new Vector3(-1, 0, 0);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            moveDir += new Vector3(1, 0, 0);
        }

        if (moveDir != Vector3.zero && IsJumping() == false)
            Jump(moveDir);
    }

    private void Jump(Vector3 targetDirection)
    {
        // atur rotasi
        // target posisi = posisi saat ini + arah * jarak
        Vector3 targetPosition = transform.position + targetDirection;
        transform.LookAt(targetPosition);

        // lonncat ke atas
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration / 2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration / 2));
        jumpSound.PlayOneShot(jumpSound.clip);
        // transform.DOMoveY(2f, 0.1f).
        // OnComplete(() => transform.DOMoveY(0, 0.1f));

        // simbol '||' = atau dari salah satu pilihan
        if (targetPosition.z <= backBoundary ||
            targetPosition.x <= leftBoundary ||
            targetPosition.x >= rightBoundary)
            return;

        if (Tree.AllPositions.Contains(targetPosition))
        {
            return;
        }

        // gerak maju, mundur, kiri, kanan
        transform.DOMoveX(targetPosition.x, moveDuration);
        transform
            .DOMoveZ(targetPosition.z, moveDuration)
            .OnComplete(UpdateTravel);
    }

    private void UpdateTravel()
    {
        currentTravel = (int)this.transform.position.z;
        if (currentTravel > maxTravel)
            maxTravel = currentTravel;

        stepText.text = "STEP: " + currentTravel.ToString();
    }

    public bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other)
    //diexecute sekali pada frame saat bersentuhan pertama kali
    //other digunakan untuk memilih antaar objek yang bersentuhan
    {
        //animasi die hanya 1 kali
        if (this.enabled == false)
            return;

        var car = other.GetComponent<Car>();
        if (car != null)
        {
            AnimateCrash(car);
        }

        // if (other.tag == "Car")
        // {
        //     // AnimateCrash(car);
        // }
    }

    private void AnimateCrash(Car car)
    {
        // var isRight = car.transform.rotation.y == 90;

        // transform.DOMoveX(isRight ? 8 : -8, 0.2f);
        // transform
        //     .DORotate(Vector3.forward * 360, 1f)
        //     .SetLoops(100, LoopType.Restart);

        // karakter jadi gepeng
        splatDead.PlayOneShot(splatDead.clip);
        transform.DOScaleY(0.1f, 0.1f);
        transform.DOScaleX(2, 0.1f);
        transform.DOScaleZ(2, 0.1f);

        //karakter jadi gak bisa gerak
        this.enabled = false;
        dieParticles.Play();
    }

    private void OnTriggerStay(Collider other)
    //diexecute setiap frame selama masih bersentuhan
    {

    }

    private void OnTriggerExit(Collider other)
    //diexecute sekali pada frame saat tidak bersentuhan
    {

    }


}
