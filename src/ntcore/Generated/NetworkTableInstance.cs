// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

// THIS FILE WAS AUTO-GENERATED BY ./ntcore/generate_topics.py. DO NOT MODIFY

using NetworkTables.Natives;

namespace NetworkTables;

public sealed partial class NetworkTableInstance
{
    private static BooleanTopic BooleanTopicCreator(string name, NetworkTableInstance instance)
    {
        BooleanTopic topic = new BooleanTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static BooleanTopic BooleanTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is BooleanTopic BooleanTopic)
        {
            return BooleanTopic;
        }
        BooleanTopic topic = new BooleanTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public BooleanTopic GetBooleanTopic(string name)
    {
        return (BooleanTopic)m_topics.AddOrUpdate(name, BooleanTopicCreator, BooleanTopicUpdator, this);
    }

    private static IntegerTopic IntegerTopicCreator(string name, NetworkTableInstance instance)
    {
        IntegerTopic topic = new IntegerTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static IntegerTopic IntegerTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is IntegerTopic IntegerTopic)
        {
            return IntegerTopic;
        }
        IntegerTopic topic = new IntegerTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public IntegerTopic GetIntegerTopic(string name)
    {
        return (IntegerTopic)m_topics.AddOrUpdate(name, IntegerTopicCreator, IntegerTopicUpdator, this);
    }

    private static FloatTopic FloatTopicCreator(string name, NetworkTableInstance instance)
    {
        FloatTopic topic = new FloatTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static FloatTopic FloatTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is FloatTopic FloatTopic)
        {
            return FloatTopic;
        }
        FloatTopic topic = new FloatTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public FloatTopic GetFloatTopic(string name)
    {
        return (FloatTopic)m_topics.AddOrUpdate(name, FloatTopicCreator, FloatTopicUpdator, this);
    }

    private static DoubleTopic DoubleTopicCreator(string name, NetworkTableInstance instance)
    {
        DoubleTopic topic = new DoubleTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static DoubleTopic DoubleTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is DoubleTopic DoubleTopic)
        {
            return DoubleTopic;
        }
        DoubleTopic topic = new DoubleTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public DoubleTopic GetDoubleTopic(string name)
    {
        return (DoubleTopic)m_topics.AddOrUpdate(name, DoubleTopicCreator, DoubleTopicUpdator, this);
    }

    private static StringTopic StringTopicCreator(string name, NetworkTableInstance instance)
    {
        StringTopic topic = new StringTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static StringTopic StringTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is StringTopic StringTopic)
        {
            return StringTopic;
        }
        StringTopic topic = new StringTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public StringTopic GetStringTopic(string name)
    {
        return (StringTopic)m_topics.AddOrUpdate(name, StringTopicCreator, StringTopicUpdator, this);
    }

    private static RawTopic RawTopicCreator(string name, NetworkTableInstance instance)
    {
        RawTopic topic = new RawTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static RawTopic RawTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is RawTopic RawTopic)
        {
            return RawTopic;
        }
        RawTopic topic = new RawTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public RawTopic GetRawTopic(string name)
    {
        return (RawTopic)m_topics.AddOrUpdate(name, RawTopicCreator, RawTopicUpdator, this);
    }

    private static BooleanArrayTopic BooleanArrayTopicCreator(string name, NetworkTableInstance instance)
    {
        BooleanArrayTopic topic = new BooleanArrayTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static BooleanArrayTopic BooleanArrayTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is BooleanArrayTopic BooleanArrayTopic)
        {
            return BooleanArrayTopic;
        }
        BooleanArrayTopic topic = new BooleanArrayTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public BooleanArrayTopic GetBooleanArrayTopic(string name)
    {
        return (BooleanArrayTopic)m_topics.AddOrUpdate(name, BooleanArrayTopicCreator, BooleanArrayTopicUpdator, this);
    }

    private static IntegerArrayTopic IntegerArrayTopicCreator(string name, NetworkTableInstance instance)
    {
        IntegerArrayTopic topic = new IntegerArrayTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static IntegerArrayTopic IntegerArrayTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is IntegerArrayTopic IntegerArrayTopic)
        {
            return IntegerArrayTopic;
        }
        IntegerArrayTopic topic = new IntegerArrayTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public IntegerArrayTopic GetIntegerArrayTopic(string name)
    {
        return (IntegerArrayTopic)m_topics.AddOrUpdate(name, IntegerArrayTopicCreator, IntegerArrayTopicUpdator, this);
    }

    private static FloatArrayTopic FloatArrayTopicCreator(string name, NetworkTableInstance instance)
    {
        FloatArrayTopic topic = new FloatArrayTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static FloatArrayTopic FloatArrayTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is FloatArrayTopic FloatArrayTopic)
        {
            return FloatArrayTopic;
        }
        FloatArrayTopic topic = new FloatArrayTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public FloatArrayTopic GetFloatArrayTopic(string name)
    {
        return (FloatArrayTopic)m_topics.AddOrUpdate(name, FloatArrayTopicCreator, FloatArrayTopicUpdator, this);
    }

    private static DoubleArrayTopic DoubleArrayTopicCreator(string name, NetworkTableInstance instance)
    {
        DoubleArrayTopic topic = new DoubleArrayTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static DoubleArrayTopic DoubleArrayTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is DoubleArrayTopic DoubleArrayTopic)
        {
            return DoubleArrayTopic;
        }
        DoubleArrayTopic topic = new DoubleArrayTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public DoubleArrayTopic GetDoubleArrayTopic(string name)
    {
        return (DoubleArrayTopic)m_topics.AddOrUpdate(name, DoubleArrayTopicCreator, DoubleArrayTopicUpdator, this);
    }

    private static StringArrayTopic StringArrayTopicCreator(string name, NetworkTableInstance instance)
    {
        StringArrayTopic topic = new StringArrayTopic(instance, NtCore.GetTopic(instance.Handle, name));
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    private static StringArrayTopic StringArrayTopicUpdator(string name, Topic existingTopic, NetworkTableInstance instance)
    {
        // Exists, but might be wrong type
        if (existingTopic is StringArrayTopic StringArrayTopic)
        {
            return StringArrayTopic;
        }
        StringArrayTopic topic = new StringArrayTopic(instance, existingTopic.Handle);
        instance.m_topicsByHandle.AddOrUpdate(topic.Handle, TopicByHandleAdder, TopicByHandleUpdater, topic);
        return topic;
    }
    public StringArrayTopic GetStringArrayTopic(string name)
    {
        return (StringArrayTopic)m_topics.AddOrUpdate(name, StringArrayTopicCreator, StringArrayTopicUpdator, this);
    }
}
