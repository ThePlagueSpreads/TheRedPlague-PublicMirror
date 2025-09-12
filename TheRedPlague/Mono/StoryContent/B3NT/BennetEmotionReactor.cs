using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheRedPlague.Mono.StoryContent.B3NT;

public class BennetEmotionReactor : MonoBehaviour
{
    public BennetController controller;
    
    private bool _readingLine;

    public bool CanReadLine => !_readingLine;
    
    public bool ReadLine(BennetLineEmotions line)
    {
        if (!CanReadLine)
            return false;
        StartCoroutine(ReadLineCoroutine(line));
        return true;
    }
    
    private IEnumerator ReadLineCoroutine(BennetLineEmotions line)
    {
        // Start
        _readingLine = true;
        
        var timeStart = Time.time;
        var queue = new Queue<BennetEmotionFrame>(line.Emotions);
        
        // Read
        while (queue.Count > 0)
        {
            var frame = queue.Dequeue();
            yield return new WaitUntil(() => Time.time >= timeStart + frame.Timestamp / 1000f);
            controller.animations.SetEmotionState(frame);
        }

        yield return new WaitForSeconds(0.5f);
        
        // Clean up
        controller.animations.SetEmotionState(default);
        _readingLine = false;
    }
}