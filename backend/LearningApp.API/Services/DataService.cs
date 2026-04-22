using LearningApp.API.Models;

namespace LearningApp.API.Services;

public class DataService
{
    private readonly List<Category> _categories;
    private readonly List<Quiz> _quizzes;
    private readonly List<MiniGame> _miniGames;

    public DataService()
    {
        _categories = BuildCategories();
        _quizzes = BuildQuizzes();
        _miniGames = BuildMiniGames();
    }

    public List<Category> GetCategories() => _categories;

    public Category? GetCategory(int id) =>
        _categories.FirstOrDefault(c => c.Id == id);

    public Lesson? GetLesson(int id)
    {
        foreach (var cat in _categories)
            foreach (var sub in cat.Subcategories)
                foreach (var lesson in sub.Lessons)
                    if (lesson.Id == id) return lesson;
        return null;
    }

    public List<Lesson> GetLessonsBySubcategory(int subcategoryId)
    {
        foreach (var cat in _categories)
            foreach (var sub in cat.Subcategories)
                if (sub.Id == subcategoryId) return sub.Lessons;
        return new List<Lesson>();
    }

    public Quiz? GetQuizByLesson(int lessonId) =>
        _quizzes.FirstOrDefault(q => q.LessonId == lessonId);

    public MiniGame? GetMiniGameByLesson(int lessonId) =>
        _miniGames.FirstOrDefault(m => m.LessonId == lessonId);

    private static List<Category> BuildCategories()
    {
        int lessonId = 1;
        int subId = 1;

        var categories = new List<Category>
        {
            new()
            {
                Id = 1, Name = "IT", Icon = "💻", Color = "#4F46E5",
                Description = "Explore the fundamentals of information technology, operating systems, and cybersecurity.",
                Subcategories = new List<Subcategory>
                {
                    new()
                    {
                        Id = subId++, CategoryId = 1, Name = "Computer Basics",
                        Description = "Learn how computers work from the ground up.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 1, Order = 1, Title = "What is a Computer?",
                                Content = "A computer is an electronic device that processes data according to instructions stored in its memory.\n\nComputers consist of hardware and software. Hardware includes physical components like the CPU, RAM, storage, and input/output devices. Software includes the operating system and applications that tell the hardware what to do.\n\nThe core of every computer is the Central Processing Unit (CPU), which performs calculations and executes instructions. Memory (RAM) temporarily stores data that the CPU is currently using, while long-term storage (HDD/SSD) keeps data persistently.\n\nComputers communicate with users through input devices (keyboard, mouse) and output devices (monitor, speakers). Together, these components create a system capable of performing complex tasks at high speed.\n\nPerformance is influenced by CPU speed, available RAM, storage throughput, and thermal design. A balanced system avoids bottlenecks, for example pairing a fast processor with very slow storage can still make the whole computer feel slow. In practice, understanding this balance helps when choosing hardware for tasks like gaming, video editing, or office productivity." },
                            new() { Id = lessonId++, SubcategoryId = 1, Order = 2, Title = "Operating Systems",
                                Content = "An operating system (OS) is the software that manages computer hardware and provides a platform for running applications.\n\nPopular operating systems include Windows, macOS, and Linux. The OS handles memory management, process scheduling, file systems, and device drivers.\n\nThe kernel is the core component of the OS — it directly manages hardware resources. On top of the kernel sits the shell and graphical user interface (GUI), which let users interact with the system.\n\nKey OS concepts include multitasking (running multiple programs simultaneously), file permissions, and virtual memory (using disk space as extra RAM).\n\nModern operating systems also focus on security through user accounts, sandboxing, process isolation, and regular patching. System logs, task managers, and service monitors are core troubleshooting tools that help diagnose crashes, high resource usage, and startup issues." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 1, Name = "Cybersecurity",
                        Description = "Understand how to protect systems and data from threats.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 2, Order = 1, Title = "Introduction to Cybersecurity",
                                Content = "Cybersecurity is the practice of protecting computer systems, networks, and data from digital attacks, theft, and damage.\n\nThe three pillars of security are Confidentiality (keeping data private), Integrity (ensuring data is not tampered with), and Availability (making sure systems are accessible when needed). These are known as the CIA Triad.\n\nCommon threats include malware (viruses, worms, ransomware), phishing attacks (tricking users into revealing credentials), and denial-of-service attacks (overwhelming a server to take it offline).\n\nSecurity professionals use tools like firewalls, antivirus software, intrusion detection systems, and encryption to defend against these threats.\n\nA strong security posture uses defense in depth, which means combining multiple layers such as endpoint protection, network controls, monitoring, and user training. Even simple habits like updating software, avoiding unknown links, and backing up data significantly reduce risk." },
                            new() { Id = lessonId++, SubcategoryId = 2, Order = 2, Title = "Encryption and Passwords",
                                Content = "Encryption transforms readable data (plaintext) into an unreadable format (ciphertext) using an algorithm and a key. Only someone with the correct key can decrypt and read the data.\n\nSymmetric encryption uses the same key for both encryption and decryption (e.g., AES). Asymmetric encryption uses a public key to encrypt and a private key to decrypt (e.g., RSA), which is the basis for HTTPS.\n\nStrong passwords are at least 12 characters long and include a mix of uppercase letters, lowercase letters, numbers, and symbols. Password managers help users store and generate strong, unique passwords for every service.\n\nTwo-factor authentication (2FA) adds an extra layer of security by requiring a second verification step beyond just a password.\n\nIn real systems, passwords should be hashed and salted rather than stored in plain text. Organizations also adopt least privilege access, account lockout policies, and phishing-resistant authentication methods to reduce account compromise." }
                        }
                    }
                }
            },
            new()
            {
                Id = 2, Name = "Programming", Icon = "🖥️", Color = "#059669",
                Description = "Learn to code from basics to advanced patterns across multiple languages.",
                Subcategories = new List<Subcategory>
                {
                    new()
                    {
                        Id = subId++, CategoryId = 2, Name = "Python Fundamentals",
                        Description = "Start your programming journey with Python.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 3, Order = 1, Title = "Variables and Data Types",
                                Content = "In Python, variables are used to store data. Unlike many languages, Python is dynamically typed — you don't need to declare the type of a variable explicitly.\n\nBasic data types include integers (int), floating-point numbers (float), strings (str), and booleans (bool). For example: age = 25, name = 'Alice', is_active = True.\n\nPython also has collection types: lists (ordered, mutable), tuples (ordered, immutable), sets (unordered, unique items), and dictionaries (key-value pairs).\n\nYou can check a variable's type with the type() function. Python variables are case-sensitive, so 'name' and 'Name' are different variables.\n\nChoosing the right data type improves code clarity and prevents bugs. For example, dictionaries are ideal for lookups by key, while lists work best for ordered sequences, and tuples are useful when values should remain unchanged." },
                            new() { Id = lessonId++, SubcategoryId = 3, Order = 2, Title = "Control Flow",
                                Content = "Control flow determines the order in which code executes. Python uses indentation (spaces or tabs) to define code blocks.\n\nConditional statements use if, elif, and else keywords. For example: if age >= 18: print('Adult') elif age >= 13: print('Teen') else: print('Child').\n\nLoops allow repeating code. The for loop iterates over a sequence: for item in list. The while loop repeats as long as a condition is true: while count < 10.\n\nBreak exits a loop early, continue skips to the next iteration, and pass is a no-op placeholder. List comprehensions provide a concise way to create lists: squares = [x**2 for x in range(10)].\n\nReadable control flow often comes from combining small conditions with clear function names. Guard clauses, early returns, and well-structured loops make programs easier to debug and maintain as complexity grows." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 2, Name = "Web Development",
                        Description = "Build websites with HTML, CSS, and JavaScript.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 4, Order = 1, Title = "HTML Basics",
                                Content = "HTML (HyperText Markup Language) is the standard language for creating web pages. It structures content using elements represented by tags.\n\nEvery HTML page has a basic structure: the DOCTYPE declaration, an html root element, a head (with metadata like title and CSS links), and a body (with visible content).\n\nCommon tags include headings (h1–h6), paragraphs (p), links (a href), images (img src), lists (ul, ol, li), and divs (div) for grouping content. Semantic tags like header, nav, main, section, article, and footer give meaning to the structure.\n\nAttributes provide additional information: class and id for CSS/JavaScript targeting, href for links, src for images, and alt for accessibility.\n\nGood HTML is both semantic and accessible. Using meaningful headings, labels, alt text, and proper document structure helps search engines understand pages and assists users who rely on screen readers or keyboard navigation." },
                            new() { Id = lessonId++, SubcategoryId = 4, Order = 2, Title = "CSS Styling",
                                Content = "CSS (Cascading Style Sheets) controls the visual appearance of HTML elements. Styles can be applied inline, in a style tag, or in an external .css file.\n\nSelectors target HTML elements: element selectors (p {}), class selectors (.classname {}), ID selectors (#id {}), and attribute selectors. The cascade determines which rule wins when multiple rules apply to the same element.\n\nThe box model describes how every element occupies space: content, padding, border, and margin from inside out. Flexbox and Grid are modern layout systems — Flexbox is one-dimensional, Grid is two-dimensional.\n\nMedia queries enable responsive design: @media (max-width: 768px) { ... } applies styles only on small screens.\n\nMaintainable CSS usually follows consistent naming conventions and reusable design tokens for spacing, color, and typography. This approach scales better for larger projects and makes theme changes faster and safer." }
                        }
                    }
                }
            },
            new()
            {
                Id = 3, Name = "Languages", Icon = "🌍", Color = "#DC2626",
                Description = "Master new human languages with grammar lessons and vocabulary practice.",
                Subcategories = new List<Subcategory>
                {
                    new()
                    {
                        Id = subId++, CategoryId = 3, Name = "English Grammar",
                        Description = "Strengthen your English grammar and writing skills.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 5, Order = 1, Title = "Tenses Overview",
                                Content = "English has 12 main tenses that express time and aspect. The three main time frames are past, present, and future, each with simple, continuous, perfect, and perfect continuous forms.\n\nSimple tenses describe facts and habits: 'She walks to school.' Continuous tenses describe actions in progress: 'She is walking to school.' Perfect tenses describe completed actions with relevance to another time: 'She has walked to school.'\n\nThe most commonly used tenses are: Simple Present (I eat), Simple Past (I ate), Simple Future (I will eat), Present Continuous (I am eating), Present Perfect (I have eaten), and Past Perfect (I had eaten).\n\nChoosing the correct tense is crucial for clear communication. Context clues like time expressions (yesterday, now, by tomorrow) help determine which tense to use.\n\nA useful learning strategy is to pair each tense with a signal phrase, such as already for present perfect or while for continuous forms. Repeated sentence transformation practice helps learners move from rule memorization to fluent usage." },
                            new() { Id = lessonId++, SubcategoryId = 5, Order = 2, Title = "Parts of Speech",
                                Content = "Every word in English belongs to a part of speech that describes its function in a sentence. The eight main parts of speech are nouns, pronouns, verbs, adjectives, adverbs, prepositions, conjunctions, and interjections.\n\nNouns name people, places, things, or ideas. Pronouns replace nouns (he, she, it, they). Verbs express actions or states of being. Adjectives describe nouns (beautiful, large). Adverbs modify verbs, adjectives, or other adverbs (quickly, very).\n\nPrepositions show relationships between elements (in, on, at, by). Conjunctions connect words, phrases, or clauses (and, but, or, because). Interjections express emotions (Oh!, Wow!).\n\nUnderstanding parts of speech helps with sentence structure, writing clearly, and learning grammar rules.\n\nMany grammar errors come from confusing role and form, such as using an adjective where an adverb is required. Sentence diagramming and targeted editing exercises build stronger awareness of how words function together." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 3, Name = "Spanish Basics",
                        Description = "Start learning Spanish vocabulary and essential phrases.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 6, Order = 1, Title = "Greetings and Introductions",
                                Content = "Spanish greetings vary by time of day and formality. Common greetings include: Hola (Hello), Buenos días (Good morning), Buenas tardes (Good afternoon), and Buenas noches (Good evening/night).\n\nTo introduce yourself say: Me llamo... (My name is...) or Soy... (I am...). To ask someone's name: ¿Cómo te llamas? (informal) or ¿Cómo se llama usted? (formal).\n\nUseful phrases: ¿Cómo estás? (How are you? – informal), ¿Cómo está usted? (formal), Bien, gracias (Fine, thank you), Mucho gusto (Nice to meet you), Encantado/a (Pleased to meet you).\n\nSpanish has two forms of 'you': tú (informal, for friends and family) and usted (formal, for strangers or elders). This affects verb conjugation throughout the language.\n\nPronunciation and listening are as important as vocabulary. Practicing regional accents, common conversational speed, and polite forms builds confidence for real interactions in travel, study, and work contexts." },
                            new() { Id = lessonId++, SubcategoryId = 6, Order = 2, Title = "Numbers and Colors",
                                Content = "Learning numbers and colors is essential for everyday communication in Spanish.\n\nNumbers 1–10: uno, dos, tres, cuatro, cinco, seis, siete, ocho, nueve, diez. 11–15: once, doce, trece, catorce, quince. Tens: veinte (20), treinta (30), cuarenta (40), cincuenta (50), cien (100).\n\nBasic colors: rojo (red), azul (blue), verde (green), amarillo (yellow), negro (black), blanco (white), naranja (orange), morado/violeta (purple), rosa (pink), gris (grey), marrón/café (brown).\n\nIn Spanish, adjectives (including colors) agree in gender and number with the noun they describe. For example: el coche rojo (the red car, masculine), la casa roja (the red house, feminine), los coches rojos (the red cars, masculine plural).\n\nFrequent real-world drills such as reading prices, dates, and addresses improve retention quickly. Pairing colors with everyday objects and speaking full phrases reinforces both vocabulary and grammar agreement." }
                        }
                    }
                }
            },
            new()
            {
                Id = 4, Name = "Networking", Icon = "🌐", Color = "#0284C7",
                Description = "Understand how computer networks and internet protocols work.",
                Subcategories = new List<Subcategory>
                {
                    new()
                    {
                        Id = subId++, CategoryId = 4, Name = "Network Fundamentals",
                        Description = "Learn the basics of computer networking.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 7, Order = 1, Title = "What is a Network?",
                                Content = "A computer network is a collection of interconnected devices that can share resources and communicate with each other. Networks range from small home setups to the global internet.\n\nTypes of networks by size: LAN (Local Area Network) covers a small area like a home or office. WAN (Wide Area Network) covers large geographic areas. MAN (Metropolitan Area Network) covers a city. The Internet is a global network of networks.\n\nNetwork devices include: routers (connect different networks), switches (connect devices within a network), access points (provide Wi-Fi), and modems (convert signals for internet access).\n\nNetworks use two main models: client-server (clients request services from dedicated servers) and peer-to-peer (devices share directly without a central server).\n\nReal network design also considers reliability, segmentation, and scalability. Concepts like VLANs, redundancy, and quality of service help keep business networks stable when many devices and applications compete for bandwidth." },
                            new() { Id = lessonId++, SubcategoryId = 7, Order = 2, Title = "IP Addressing",
                                Content = "Every device on a network is identified by an IP address. IPv4 addresses consist of four octets separated by dots (e.g., 192.168.1.1), with a range from 0 to 255 in each octet, providing about 4.3 billion unique addresses.\n\nIPv6 uses 128-bit addresses written in hexadecimal (e.g., 2001:0db8:85a3::8a2e:0370:7334) to solve the address exhaustion problem of IPv4.\n\nSubnet masks divide IP addresses into network and host portions. For example, a mask of 255.255.255.0 (/24) means the first 24 bits identify the network and the last 8 bits identify individual hosts.\n\nPrivate IP ranges (not routable on the internet): 192.168.x.x, 172.16–31.x.x, and 10.x.x.x. NAT (Network Address Translation) allows multiple devices to share one public IP.\n\nNetwork troubleshooting often starts with tools like ping, tracert or traceroute, and ipconfig or ifconfig. Understanding subnetting and default gateways makes it easier to diagnose why a device can reach local hosts but not the internet." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 4, Name = "TCP/IP Protocols",
                        Description = "Dive into the protocols that power the internet.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 8, Order = 1, Title = "The OSI Model",
                                Content = "The OSI (Open Systems Interconnection) model is a conceptual framework that describes how network communication works in 7 layers.\n\nLayer 7 – Application: User-facing protocols (HTTP, FTP, DNS, SMTP). Layer 6 – Presentation: Data translation, encryption (SSL/TLS). Layer 5 – Session: Managing sessions between applications. Layer 4 – Transport: Reliable data transfer (TCP, UDP). Layer 3 – Network: Logical addressing and routing (IP). Layer 2 – Data Link: Physical addressing (MAC), error detection (Ethernet). Layer 1 – Physical: Bits over the wire (cables, signals).\n\nA helpful mnemonic: 'All People Seem To Need Data Processing' (Application → Physical) or 'Please Do Not Throw Sausage Pizza Away' (Physical → Application).\n\nEach layer communicates with the layer directly above and below it, adding or removing headers as data moves through the stack.\n\nThe OSI model is especially useful as a troubleshooting checklist. For example, verify cables and signal first, then addressing and routing, and finally application-level settings, rather than guessing at random layers." },
                            new() { Id = lessonId++, SubcategoryId = 8, Order = 2, Title = "TCP vs UDP",
                                Content = "TCP (Transmission Control Protocol) and UDP (User Datagram Protocol) are the two main transport-layer protocols.\n\nTCP is connection-oriented: it establishes a connection via a three-way handshake (SYN, SYN-ACK, ACK), guarantees delivery, ensures packets arrive in order, and performs error checking and retransmission. Used for web browsing (HTTP/HTTPS), email, and file transfers where reliability matters.\n\nUDP is connectionless: it sends packets without establishing a connection, offers no delivery guarantee or ordering, but is much faster with lower overhead. Used for video streaming, online gaming, DNS lookups, and VoIP where speed matters more than perfect reliability.\n\nCommon port numbers: HTTP 80, HTTPS 443, FTP 21, SSH 22, DNS 53, SMTP 25. A socket is a combination of IP address and port (e.g., 192.168.1.1:443).\n\nProtocol choice depends on the business requirement. Applications that prioritize correctness and complete delivery usually prefer TCP, while low-latency real-time apps often use UDP with custom recovery or buffering strategies." }
                        }
                    }
                }
            },
            new()
            {
                Id = 5, Name = "AI & ML", Icon = "🤖", Color = "#7C3AED",
                Description = "Discover artificial intelligence and machine learning concepts and applications.",
                Subcategories = new List<Subcategory>
                {
                    new()
                    {
                        Id = subId++, CategoryId = 5, Name = "Machine Learning Basics",
                        Description = "Understand the foundations of machine learning.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 9, Order = 1, Title = "What is Machine Learning?",
                                Content = "Machine learning (ML) is a subset of artificial intelligence where systems learn from data to improve their performance on tasks without being explicitly programmed.\n\nThere are three main types of ML: Supervised learning (the model learns from labeled examples — input/output pairs), Unsupervised learning (the model finds patterns in unlabeled data), and Reinforcement learning (an agent learns by interacting with an environment and receiving rewards or penalties).\n\nThe typical ML workflow: collect and prepare data → choose a model → train the model → evaluate performance → tune hyperparameters → deploy.\n\nCommon applications include image recognition, spam detection, recommendation systems, fraud detection, and natural language processing. Python libraries like scikit-learn, TensorFlow, and PyTorch are widely used.\n\nHigh-quality data is often more important than choosing a complex algorithm. Teams spend significant effort on labeling, cleaning, feature engineering, and monitoring drift after deployment to keep model performance stable." },
                            new() { Id = lessonId++, SubcategoryId = 9, Order = 2, Title = "Supervised Learning",
                                Content = "Supervised learning trains a model on labeled data — each training example has an input (features) and a known output (label). The model learns to map inputs to outputs and then predicts labels for new, unseen data.\n\nTwo main tasks: Classification (predicting a category, e.g., spam/not spam, cat/dog) and Regression (predicting a continuous value, e.g., house price, temperature).\n\nCommon algorithms: Linear Regression (for regression), Logistic Regression (for classification despite the name), Decision Trees, Random Forests, Support Vector Machines (SVM), and k-Nearest Neighbors (kNN).\n\nKey concepts: Training set (data used to train), Test set (data used to evaluate), Overfitting (model learns training data too well, performs poorly on new data), Underfitting (model is too simple to capture patterns), and Cross-validation (technique to assess generalizability).\n\nModel evaluation should match the task, such as precision and recall for imbalanced classification or MAE and RMSE for regression. A clear baseline model is valuable before adopting more advanced approaches." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 5, Name = "Neural Networks",
                        Description = "Explore deep learning and neural network architectures.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 10, Order = 1, Title = "How Neural Networks Work",
                                Content = "A neural network is a computational model inspired by the human brain. It consists of layers of interconnected nodes (neurons) that process information.\n\nLayers: The Input layer receives raw data. Hidden layers perform transformations (a deep network has multiple hidden layers). The Output layer produces the final prediction.\n\nEach connection has a weight that is adjusted during training. Each neuron applies an activation function (like ReLU or Sigmoid) to introduce non-linearity, enabling the network to learn complex patterns.\n\nTraining uses backpropagation: the network makes a prediction, calculates the error using a loss function, then propagates the error backward to update weights using an optimizer like Gradient Descent. This process repeats over many iterations (epochs) until the model converges.\n\nTraining quality also depends on learning rate schedules, normalization, regularization, and sufficient validation. Without these controls, networks may overfit quickly or fail to converge to useful solutions." },
                            new() { Id = lessonId++, SubcategoryId = 10, Order = 2, Title = "Deep Learning Applications",
                                Content = "Deep learning (DL) uses neural networks with many layers to automatically learn hierarchical representations of data. It has transformed many fields.\n\nConvolutional Neural Networks (CNNs) are specialized for image data — they use convolution operations to detect features like edges, shapes, and textures. Used in image classification, object detection, and medical imaging.\n\nRecurrent Neural Networks (RNNs) and LSTMs process sequential data (text, speech, time series). Transformers have largely replaced RNNs for NLP tasks — they power large language models like GPT and BERT.\n\nGenerative AI includes GANs (Generative Adversarial Networks) for creating images, and diffusion models for image/video generation. Large Language Models (LLMs) can generate text, write code, translate languages, and answer questions.\n\nDeployment adds practical constraints such as latency, memory use, explainability, and fairness. Production systems often combine deep models with monitoring, fallback rules, and periodic retraining pipelines." }
                        }
                    }
                }
            },
            new()
            {
                Id = 6, Name = "Soft Skills", Icon = "💬", Color = "#F59E0B",
                Description = "Develop interpersonal skills, communication, leadership, and emotional intelligence.",
                Subcategories = new List<Subcategory>
                {
                    new()
                    {
                        Id = subId++, CategoryId = 6, Name = "Communication",
                        Description = "Improve how you express ideas and listen actively.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 11, Order = 1, Title = "Effective Communication Skills",
                                Content = "Effective communication is the ability to convey information clearly, listen actively, and adapt your message to your audience. It is one of the most sought-after skills in the workplace.\n\nThe communication process involves a sender, message, medium, receiver, and feedback. Barriers like noise, assumptions, language differences, and emotional state can distort messages.\n\nActive listening means fully concentrating on the speaker — not just hearing words but understanding meaning. Techniques: maintain eye contact, nod, ask clarifying questions, and paraphrase to confirm understanding.\n\nNon-verbal communication (body language, facial expressions, tone of voice, gestures) can reinforce or contradict verbal messages. Studies suggest non-verbal cues carry a large portion of meaning in face-to-face interactions.\n\nStrong communicators also tailor depth and tone based on audience expertise. A technical audience may need detailed evidence, while executives often need concise summaries focused on impact, risk, and decisions." },
                            new() { Id = lessonId++, SubcategoryId = 11, Order = 2, Title = "Public Speaking",
                                Content = "Public speaking is the ability to communicate ideas effectively to a group. It is consistently rated as one of the most valuable professional skills.\n\nPreparing a speech: Know your audience, define your purpose (inform, persuade, entertain), structure your content (opening, body, conclusion), and practice out loud multiple times.\n\nDelivery techniques: Speak clearly and at a moderate pace, use pauses for emphasis, vary your tone and volume, maintain eye contact with different parts of the audience, and use gestures naturally.\n\nManaging nerves: Deep breathing calms the nervous system. Reframe anxiety as excitement. Visualize success. The more you speak publicly, the more comfortable you become — consistent practice is the key.\n\nThe opening and closing are most memorable. Start with a hook (story, question, surprising fact) and end with a memorable call to action or summary.\n\nPost-presentation reflection is a key growth loop. Recording yourself, reviewing pacing and filler words, and collecting audience feedback can rapidly improve clarity and confidence over repeated talks." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 6, Name = "Leadership",
                        Description = "Learn what makes an effective leader and how to inspire teams.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 12, Order = 1, Title = "Leadership Styles",
                                Content = "Leadership style is how a leader directs, motivates, and manages people. Different styles suit different situations and team types.\n\nAutocratic leadership: The leader makes decisions unilaterally with little input from team members. Effective in crisis situations or when quick decisions are needed, but can reduce morale long-term.\n\nDemocratic (Participative) leadership: The leader involves team members in decision-making. Increases buy-in, creativity, and morale, but can be slow.\n\nTransformational leadership: The leader inspires and motivates through a compelling vision and personal example. Effective for driving innovation and change.\n\nServant leadership: The leader puts the team's needs first, removes obstacles, and develops people. Associated with high trust and long-term performance.\n\nSituational leadership theory suggests effective leaders adapt their style based on the competence and commitment level of each team member.\n\nNo style is universally best. Effective leaders align approach with team maturity, urgency, and organizational culture, then communicate expectations clearly so people understand both direction and rationale." },
                            new() { Id = lessonId++, SubcategoryId = 12, Order = 2, Title = "Emotional Intelligence",
                                Content = "Emotional intelligence (EI or EQ) is the ability to recognize, understand, manage, and use emotions — both your own and others' — effectively.\n\nDaniel Goleman's EI model has five components: Self-awareness (knowing your emotions), Self-regulation (managing your emotions), Motivation (intrinsic drive to achieve), Empathy (understanding others' feelings), and Social skills (managing relationships).\n\nHigh EQ leaders build stronger teams, handle conflict better, stay calm under pressure, and inspire trust. EQ is often cited as a stronger predictor of leadership success than IQ.\n\nDeveloping EI: Practice mindfulness to increase self-awareness. Keep a journal to reflect on emotional reactions. Seek feedback. Practice empathy by actively trying to see situations from others' perspectives. Pause before reacting in emotionally charged situations.\n\nEmotional intelligence is trainable through deliberate habits such as pause-and-label techniques, reflective feedback sessions, and conflict debriefs. Over time, these practices improve trust, resilience, and collaboration." }
                        }
                    }
                }
            },
            new()
            {
                Id = 7, Name = "Hardware", Icon = "🔧", Color = "#6B7280",
                Description = "Learn about computer hardware components, architecture, and storage technologies.",
                Subcategories = new List<Subcategory>
                {
                    new()
                    {
                        Id = subId++, CategoryId = 7, Name = "PC Components",
                        Description = "Understand the parts that make up a computer.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 13, Order = 1, Title = "CPU and Motherboard",
                                Content = "The CPU (Central Processing Unit) is the brain of the computer. It executes instructions and performs calculations. Key specifications include clock speed (GHz), number of cores, cache size, and architecture.\n\nModern CPUs have multiple cores (2, 4, 8, 16+) allowing true parallel processing. Hyper-threading (Intel) / SMT (AMD) allows each core to handle two threads simultaneously. The cache (L1, L2, L3) provides ultra-fast temporary storage near the processor.\n\nThe motherboard is the main circuit board that connects all components. It contains the CPU socket, RAM slots, PCIe slots (for GPU and other expansion cards), M.2 slots (for NVMe SSDs), SATA ports, and I/O ports. The chipset manages communication between the CPU and other components.\n\nThe BIOS/UEFI firmware initializes hardware during startup and provides settings for the system before the OS loads.\n\nCompatibility planning is essential during builds. Socket type, chipset support, memory standards, and power delivery must align, and firmware updates can improve hardware stability, performance, and device support." },
                            new() { Id = lessonId++, SubcategoryId = 13, Order = 2, Title = "RAM and GPU",
                                Content = "RAM (Random Access Memory) is the short-term memory of the computer. It temporarily stores data that the CPU is actively using. More RAM allows more programs to run simultaneously without slowdown.\n\nRAM types: DDR4 and DDR5 are current standards. Key specs are capacity (GB), speed (MHz/MT/s), and latency (CL timings). RAM is volatile — data is lost when power is removed.\n\nThe GPU (Graphics Processing Unit) was originally designed for rendering graphics but is now also used for AI, scientific computing, and cryptocurrency mining. A GPU contains thousands of smaller cores optimized for parallel tasks.\n\nDedicated GPUs (discrete) have their own VRAM (video memory). Integrated graphics share system RAM and are built into the CPU or motherboard. For gaming and machine learning, a powerful dedicated GPU is essential.\n\nThermal management (heatsinks, fans, liquid cooling) is critical for both CPU and GPU to prevent thermal throttling.\n\nCapacity planning should reflect workload type. Creative applications and machine learning often benefit from larger memory pools and faster GPUs, while office workloads may prioritize efficiency and integrated graphics." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 7, Name = "Storage Technologies",
                        Description = "Explore different types of storage and how they work.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 14, Order = 1, Title = "HDD vs SSD",
                                Content = "Hard Disk Drives (HDDs) store data on spinning magnetic platters read by a mechanical arm. They offer large capacities at low cost but are slower and more fragile due to moving parts. Typical speeds: 80–160 MB/s.\n\nSolid State Drives (SSDs) store data in NAND flash memory chips with no moving parts. They are much faster, more durable, lighter, and quieter but cost more per gigabyte.\n\nSSD types: SATA SSDs use the same connector as HDDs (up to ~550 MB/s). NVMe SSDs connect via the PCIe bus (M.2 slot) and reach 3,000–7,000 MB/s (Gen 3–5).\n\nHDDs are still used where large, low-cost storage is needed (backup drives, NAS, servers). SSDs are standard for OS and application drives. Many systems use both — SSD for speed and HDD for bulk storage.\n\nWear leveling and TRIM are features of SSDs that distribute writes evenly and maintain performance over time.\n\nA practical storage strategy is to place operating system and active projects on SSD storage, while archiving large media and backups on HDDs. This hybrid approach balances speed, cost, and total capacity." },
                            new() { Id = lessonId++, SubcategoryId = 14, Order = 2, Title = "RAID and Backup Strategies",
                                Content = "RAID (Redundant Array of Independent Disks) combines multiple physical drives into one logical unit for performance or redundancy.\n\nRAID 0 (Striping): Data split across drives for maximum performance. No redundancy — if one drive fails, all data is lost. RAID 1 (Mirroring): Data duplicated on two drives. If one fails, the other takes over. RAID 5: Data striped across 3+ drives with parity, tolerating one drive failure. RAID 6: Like RAID 5 but tolerates two drive failures. RAID 10: Combines mirroring and striping — fast and redundant but requires 4+ drives.\n\nRAID is not a backup — it protects against hardware failure but not accidental deletion, ransomware, or site disasters. The 3-2-1 backup rule: keep 3 copies of data, on 2 different media types, with 1 copy offsite (e.g., cloud).\n\nCloud storage (Google Drive, OneDrive, Backblaze) provides offsite backups with automatic syncing.\n\nRecovery planning should include regular restore testing, versioned backups, and clear recovery objectives such as RPO and RTO. A backup that has never been tested may fail when it is needed most." }
                        }
                    }
                }
            }
        };

        var additionalLessons = new Dictionary<int, (string Title, string Content)>
        {
            { 1, ("Computer Hardware Components", "Computer hardware is often grouped into four functional areas: processing, memory, storage, and input/output.\n\nProcessing is handled mainly by the CPU, which executes program instructions. Memory is primarily RAM, which stores active data for fast access. Storage includes SSDs and HDDs, which keep data persistently. Input/output devices allow users and external systems to interact with the computer.\n\nPerformance depends on how well these components work together. A powerful CPU can still feel slow if RAM is limited or storage is outdated. Understanding component roles helps users diagnose bottlenecks and plan upgrades effectively.\n\nCommon troubleshooting examples include high disk usage causing system lag, insufficient RAM leading to excessive swapping, and thermal throttling reducing processor speed under load.\n\nWhen building or upgrading systems, users should balance budget with workload needs. Office tasks may prioritize reliability and responsiveness, while gaming and content creation often require stronger CPUs, more RAM, and dedicated GPUs.") },
            { 2, ("Network Security Fundamentals", "Network security focuses on protecting data as it moves between devices, servers, and services.\n\nCore controls include firewalls, secure network segmentation, intrusion detection, VPNs, and strong authentication. Segmenting networks limits how far an attacker can move after a breach.\n\nZero trust is a modern approach where no user or device is trusted by default, even inside corporate networks. Access is granted only after identity verification, device checks, and policy evaluation.\n\nLogging and monitoring are critical. Security teams analyze unusual traffic patterns, repeated login failures, and suspicious outbound connections to detect active threats early.\n\nPractical protection also includes patching network devices, disabling unused ports, enforcing MFA for remote access, and routinely testing incident response procedures.") },
            { 3, ("Functions and Modules", "Functions help organize Python code into reusable blocks that perform specific tasks.\n\nA function is defined with def, can accept parameters, and can return values. Clear function names and small responsibilities improve readability and testing.\n\nModules are Python files that group related functions, classes, and constants. You can reuse module code with import statements, which encourages maintainable project structure.\n\nPython also provides standard library modules such as math, datetime, and json, reducing the need to reinvent common functionality.\n\nAs projects grow, separating logic into modules and packages makes collaboration easier, reduces duplication, and supports cleaner dependency boundaries.") },
            { 4, ("JavaScript Fundamentals", "JavaScript adds behavior and interactivity to web pages.\n\nCore concepts include variables, functions, conditionals, loops, and events. Developers often manipulate page content with the DOM (Document Object Model), responding to user actions like clicks and form input.\n\nModern JavaScript uses features such as let/const, arrow functions, template literals, and modules. Async programming with promises and async/await is essential for API calls and non-blocking UI updates.\n\nUnderstanding event propagation, state updates, and error handling helps prevent common bugs in dynamic interfaces.\n\nA strong front-end foundation combines semantic HTML, maintainable CSS, and JavaScript logic that is readable, testable, and accessible.") },
            { 5, ("Sentence Structure", "English sentence structure usually follows Subject + Verb + Object order.\n\nSimple sentences contain one independent clause. Compound sentences connect two independent clauses with conjunctions. Complex sentences combine independent and dependent clauses.\n\nCorrect punctuation and conjunction choice affect clarity. Misplaced modifiers or run-on sentences can make writing confusing.\n\nWriters improve structure by varying sentence length, using transitions, and checking whether each sentence communicates one clear idea.\n\nEditing for grammar, punctuation, and flow is a key step in producing professional emails, essays, and reports.") },
            { 6, ("Common Spanish Verbs", "Spanish verbs are central to daily communication, especially high-frequency verbs like ser, estar, tener, ir, and hacer.\n\nSer and estar both mean 'to be' but are used in different contexts: ser for identity and permanent traits, estar for temporary states and location.\n\nVerb conjugation changes based on the subject pronoun and tense. Learning present-tense forms of common verbs provides a strong conversational base.\n\nUseful phrases include tengo hambre (I am hungry), voy al trabajo (I am going to work), and estamos listos (we are ready).\n\nConsistent speaking practice with common verbs improves fluency much faster than memorizing isolated vocabulary lists.") },
            { 7, ("Network Topologies", "Network topology describes how devices are arranged and connected.\n\nCommon topologies include star, bus, ring, mesh, and hybrid. Star topology is widely used in modern LANs because it is easier to manage and isolate failures.\n\nMesh networks provide high redundancy because devices have multiple paths, but they are more complex and expensive to deploy.\n\nPhysical topology describes cable layout, while logical topology describes how data flows across the network.\n\nChoosing topology depends on scale, budget, reliability requirements, and expected traffic patterns.") },
            { 8, ("Application Layer Protocols", "Application layer protocols support user-facing network services.\n\nHTTP/HTTPS transfer web content, DNS resolves domain names to IP addresses, SMTP sends email, and FTP/SFTP handle file transfer.\n\nHTTPS combines HTTP with TLS encryption to protect confidentiality and integrity in transit.\n\nProtocol troubleshooting often involves checking ports, certificates, DNS records, and request/response status codes.\n\nUnderstanding protocol behavior helps engineers diagnose outages faster and design secure, reliable distributed systems.") },
            { 9, ("Model Evaluation Metrics", "Machine learning metrics measure how well models perform on their tasks.\n\nFor classification, common metrics include accuracy, precision, recall, F1-score, and ROC-AUC. Accuracy alone can be misleading on imbalanced datasets.\n\nFor regression, MAE, MSE, and RMSE quantify prediction error. Lower values indicate better fit, but interpretability and business tolerance matter.\n\nConfusion matrices reveal where classification models make mistakes, guiding threshold tuning and feature improvements.\n\nGood evaluation also includes validation on unseen data and monitoring production performance for drift over time.") },
            { 10, ("Training Neural Networks Effectively", "Training neural networks requires careful control of optimization and generalization.\n\nKey factors include learning rate, batch size, optimizer choice, and regularization methods like dropout and weight decay.\n\nValidation data is used to detect overfitting and guide early stopping. Data augmentation can improve robustness for vision and speech tasks.\n\nHardware acceleration with GPUs significantly reduces training time for deep models.\n\nSuccessful training pipelines also include experiment tracking, reproducibility practices, and clear model versioning.") },
            { 11, ("Written Communication at Work", "Written communication is essential for documentation, email, reports, and collaborative planning.\n\nStrong writing is clear, concise, and audience-focused. Messages should state context, purpose, requested action, and deadlines.\n\nStructure improves readability: use informative headings, short paragraphs, and bullet points for key decisions.\n\nTone matters. Professional communication avoids ambiguity and remains respectful, especially in cross-functional or remote teams.\n\nReviewing drafts for clarity and factual accuracy prevents misunderstandings and saves time in later discussions.") },
            { 12, ("Decision-Making for Leaders", "Leadership includes making timely, well-informed decisions under uncertainty.\n\nEffective decision-making combines data analysis, stakeholder input, risk assessment, and clear prioritization.\n\nFrameworks such as pros/cons matrices, impact-vs-effort grids, and pre-mortems help teams evaluate options objectively.\n\nAfter deciding, leaders communicate rationale, ownership, and success criteria so teams can execute confidently.\n\nStrong leaders also review outcomes, capture lessons learned, and adjust strategy when new evidence appears.") },
            { 13, ("Power Supply and Cooling", "The power supply unit (PSU) converts wall AC power into stable DC voltages for PC components.\n\nSelecting the right PSU involves sufficient wattage, quality protections, and efficiency certification (such as 80 PLUS ratings).\n\nCooling systems include air coolers, case airflow design, and liquid cooling solutions. Proper airflow direction and dust management are critical for long-term stability.\n\nHigh temperatures can reduce performance through thermal throttling and shorten hardware lifespan.\n\nA reliable build balances performance with thermal and power headroom for future upgrades.") },
            { 14, ("Data Reliability and Recovery", "Data reliability combines fault tolerance, monitoring, and tested recovery workflows.\n\nSnapshots, replication, and versioned backups reduce risk from accidental changes and system failures. Each method addresses different failure scenarios.\n\nRecovery objectives matter: RPO defines acceptable data loss, while RTO defines acceptable downtime.\n\nOrganizations should routinely test restore procedures, not just backup jobs, to ensure recovery works during real incidents.\n\nA resilient storage strategy includes encryption, access controls, and documented incident playbooks.") }
        };

        foreach (var category in categories)
        {
            foreach (var subcategory in category.Subcategories)
            {
                if (!additionalLessons.TryGetValue(subcategory.Id, out var lesson))
                {
                    continue;
                }

                subcategory.Lessons.Add(new Lesson
                {
                    Id = lessonId++,
                    SubcategoryId = subcategory.Id,
                    Order = subcategory.Lessons.Count + 1,
                    Title = lesson.Title,
                    Content = lesson.Content
                });
            }
        }

        var moreLessons = new Dictionary<int, List<(string Title, string Content)>>
        {
            { 1, new List<(string, string)>
            {
                ("File Systems", "A file system is the method an operating system uses to organize, store, name, and retrieve files on a storage device.\n\nCommon file systems include NTFS (Windows), ext4 (Linux), and APFS (macOS). FAT32 is an older cross-platform format still used on USB drives for broad compatibility. exFAT is a modern alternative for flash drives with no 4 GB file-size cap.\n\nFiles are organized in a hierarchical directory tree. Paths describe a file's location: absolute paths start from the root (C:\\ on Windows, / on Linux/macOS), while relative paths are relative to the current working directory.\n\nJournaling maintains a log of changes before applying them, allowing the file system to recover to a consistent state after unexpected shutdowns. This protection is included in NTFS, ext4, and most modern file systems.\n\nChoosing the right file system matters for performance, reliability, and cross-platform compatibility. For example, NTFS supports large files and fine-grained permissions but is not natively writable on macOS without third-party tools, while exFAT offers broad compatibility for removable storage."),
                ("Input and Output Devices", "Input devices allow users to send data and commands to a computer. Common examples include keyboards, mice, touchscreens, scanners, microphones, and webcams. Each converts physical input into digital signals the computer can process.\n\nOutput devices deliver processed information from the computer to users. Monitors, printers, speakers, and projectors are typical output devices. Some devices like touchscreens serve as both input and output simultaneously.\n\nDevices connect via USB (A, C, 3.2, 4), Bluetooth, HDMI, DisplayPort, or proprietary connectors. Plug-and-play systems automatically detect and install drivers for connected devices.\n\nMonitor specifications that matter: resolution (1920×1080, 2560×1440, 3840×2160), refresh rate (60 Hz, 144 Hz, 240 Hz — higher rates reduce motion blur in gaming), panel type (IPS for color accuracy, TN for low response time, OLED for contrast), and response time.\n\nAssistive technology expands I/O possibilities for users with disabilities. Screen readers, braille displays, eye-tracking devices, and voice control software all serve as alternative I/O methods, making computing more accessible to a wider population."),
                ("Computer Networks Overview", "A computer network connects devices so they can share information and resources. Networks range from two computers sharing a printer to billions of devices forming the global internet.\n\nNetworks are built from physical media (cables, fiber, wireless signals), network interface cards (NICs), and intermediary devices like switches and routers. Each layer handles different responsibilities in delivering data reliably from source to destination.\n\nData is broken into packets — small units that travel independently and are reassembled at the destination. This packet-switching approach is more efficient and resilient than dedicated circuit switching.\n\nProtocols define rules for how devices communicate. The TCP/IP suite governs the internet, while Wi-Fi standards (IEEE 802.11) govern wireless communication. Protocols at each layer build on the services provided by the layer below.\n\nUnderstanding basic networking helps troubleshoot connectivity issues, set up shared resources, and improve security. Concepts like IP addressing, DNS, and DHCP are foundational for both everyday users and IT professionals."),
                ("Software Types and Licenses", "Software can be categorized by function: system software (OS, drivers, utilities) manages hardware and provides a platform, while application software (browsers, word processors, games) serves end-user tasks.\n\nLicensing determines how software may be used, modified, and distributed. Proprietary software is owned by its creator and restricts modification and redistribution. Free and open-source software (FOSS) allows users to view, modify, and share the source code under defined conditions.\n\nCommon license types include freeware (free to use, source closed), shareware (trial then pay), open-source (GPL requires derivatives to remain open; MIT and Apache allow more permissive use), and subscription licenses (SaaS models like Adobe Creative Cloud).\n\nSoftware distribution has evolved from physical media to digital downloads and cloud-based services. SaaS delivers applications via the web, reducing installation overhead and enabling automatic updates.\n\nUnderstanding licenses is important for legal compliance, especially in business environments. Using software without proper licensing can result in legal liability, while open-source licenses have their own conditions that must be followed when distributing derived works."),
                ("Binary and Number Systems", "Computers store and process all information as binary — sequences of 0s and 1s called bits. Eight bits form one byte, which can represent 256 different values (0–255).\n\nBinary (base-2) uses only two digits. Counting in binary: 0, 1, 10, 11, 100, 101... Each bit position represents a power of 2 (1, 2, 4, 8, 16...). To convert binary to decimal, multiply each bit by its positional power of 2 and sum the results. For example, 1101 = 8 + 4 + 0 + 1 = 13.\n\nHexadecimal (base-16) uses digits 0–9 and letters A–F. It is commonly used to represent binary data more compactly — each hex digit represents exactly 4 bits. Memory addresses, color codes (#FF5733), and network MAC addresses are often written in hex.\n\nOctal (base-8) uses digits 0–7 and was historically common in computing. Understanding hex is far more practical today.\n\nConversion between bases is a key skill. Understanding binary helps with topics like networking (subnet masks are binary values), color in graphics (RGB values), and low-level programming where bit manipulation is needed.")
            }},
            { 2, new List<(string, string)>
            {
                ("Social Engineering Attacks", "Social engineering attacks exploit human psychology rather than technical vulnerabilities to gain unauthorized access to systems or information. Because they bypass technical controls, they are among the most effective attack methods.\n\nPhishing is the most common form — attackers send emails impersonating trusted sources (banks, IT support, executives) to steal credentials or install malware. Spear phishing targets specific individuals with personalized content. Vishing uses voice calls; smishing uses SMS.\n\nPretexting involves creating a fabricated scenario to manipulate victims. For example, an attacker may impersonate an IT technician requesting a password to 'reset an account' urgently.\n\nTailgating (piggybacking) is a physical attack where an unauthorized person follows an authorized individual through a secured door. Baiting leaves infected USB drives in public places for curious victims to plug in.\n\nDefenses include security awareness training, multi-factor authentication, email filtering, clear identity-verification policies, and a culture that encourages reporting suspicious contacts without fear of blame. The most important defense is a well-trained, skeptical workforce."),
                ("Types of Malware", "Malware (malicious software) is any program designed to damage systems, steal data, or gain unauthorized access. Understanding malware types helps with detection and building effective defenses.\n\nViruses attach themselves to legitimate files and spread when those files are executed or shared. Worms self-replicate and spread across networks without any user interaction. Trojans disguise themselves as legitimate software but execute malicious code when run.\n\nRansomware encrypts the victim's files and demands payment for the decryption key. It is one of the most financially damaging malware types, targeting individuals, businesses, and critical infrastructure. Spyware silently monitors user activity and exfiltrates credentials and sensitive data.\n\nRootkits hide deep in the OS to maintain persistent, hidden access. Keyloggers record keystrokes to capture passwords. Botnets are networks of infected machines controlled remotely for spam, DDoS attacks, or cryptocurrency mining.\n\nDefense-in-depth includes keeping software updated, using reputable antivirus and EDR tools, avoiding suspicious downloads, practicing least-privilege access, and maintaining offline backups as a ransomware recovery strategy."),
                ("Network Security Tools", "Network security tools help detect, prevent, and respond to threats targeting networked systems. A layered combination of tools is more effective than relying on any single solution.\n\nFirewalls filter traffic based on rules. Stateful firewalls track active connections. Next-generation firewalls (NGFWs) add application awareness, deep packet inspection, and integrated threat intelligence feeds.\n\nIntrusion Detection Systems (IDS) monitor network traffic for known attack signatures or anomalies and alert administrators. Intrusion Prevention Systems (IPS) automatically block suspicious traffic in real time.\n\nVulnerability scanners (Nessus, OpenVAS) probe systems for known weaknesses. Penetration testing tools (Metasploit, Burp Suite) simulate real attacks to find exploitable flaws before attackers do.\n\nSIEM (Security Information and Event Management) systems aggregate logs from multiple sources, correlate events, and provide real-time analysis and alerting. Effective security operations centers (SOCs) combine SIEM data with threat intelligence and human analyst review to detect and respond to incidents quickly."),
                ("Security Policies and Compliance", "Security policies define the rules, procedures, and responsibilities that govern how an organization protects its information assets. They establish accountability and a shared framework for decision-making.\n\nKey policy areas include: acceptable use (what users may do with company systems), password management (complexity, length, rotation), data classification (labeling and handling data by sensitivity), and incident response (steps to follow when a breach occurs).\n\nCompliance frameworks provide structured security standards. Common ones include ISO 27001 (information security management), SOC 2 (service organization controls), GDPR (data privacy in the EU), HIPAA (US healthcare data), and PCI DSS (payment card data protection).\n\nRisk management involves identifying assets, assessing threats and vulnerabilities, estimating likelihood and impact, and selecting controls to reduce risk to acceptable levels. Risk registers document these assessments and drive prioritization.\n\nEffective governance embeds security into business processes rather than treating it as an afterthought. Regular audits, staff training, policy reviews, and executive sponsorship keep the security posture aligned with evolving threats and organizational changes."),
                ("Incident Response", "Incident response (IR) is the structured approach to detecting, containing, and recovering from cybersecurity incidents while minimizing damage and learning from each event.\n\nThe IR lifecycle: Preparation → Identification → Containment → Eradication → Recovery → Lessons Learned. Each phase has defined objectives, activities, and tools.\n\nPreparation involves creating an IR plan, assembling a response team, deploying logging and monitoring, and rehearsing with tabletop exercises. Identification means detecting and validating that an incident has occurred — SIEM alerts, EDR telemetry, and user reports are common detection sources.\n\nContainment limits damage — isolating infected systems from the network. Eradication removes root causes (malware, compromised accounts, exploited vulnerabilities). Recovery restores systems to normal operation from verified clean backups. Monitoring closely for recurrence is essential in the immediate post-recovery period.\n\nThe Lessons Learned phase is critical: post-incident reports identify root causes, missed signals, and improvements. Near-miss incidents are equally valuable learning opportunities. Mature IR programs treat each incident as an opportunity to strengthen defenses.")
            }},
            { 3, new List<(string, string)>
            {
                ("Object-Oriented Programming", "Object-Oriented Programming (OOP) organizes code around objects — entities that combine data (attributes) and behavior (methods) into a single unit called a class.\n\nA class is a blueprint: class Dog: / def __init__(self, name): / self.name = name / def bark(self): / print('Woof!'). An object is an instance: rex = Dog('Rex'). Calling rex.bark() executes the method on that specific instance.\n\nFour pillars of OOP: Encapsulation (bundling data and methods, hiding internal state behind a public interface), Inheritance (child classes inherit attributes and methods from a parent: class Puppy(Dog)), Polymorphism (different classes respond to the same method call in different ways), and Abstraction (hiding complex implementation behind a simple interface).\n\nSpecial (dunder) methods allow custom objects to integrate with Python's built-in functions: __str__ controls print output, __len__ supports len(), __eq__ controls equality checks, and __repr__ provides a developer-friendly representation.\n\nOOP improves code organization, reusability, and maintainability as projects grow. Modeling domain concepts as classes creates code that mirrors real-world relationships and is intuitive for teams to understand and extend."),
                ("File Handling in Python", "Python provides built-in functions to read from and write to files, enabling programs to persist and exchange data between runs.\n\nThe open() function opens a file with a specified mode: 'r' (read), 'w' (write, overwrites), 'a' (append), 'rb'/'wb' (binary). Always use the with statement to ensure files are closed automatically: with open('data.txt', 'r') as f: content = f.read().\n\nReading: f.read() reads the entire file as a string; f.readline() reads one line; f.readlines() returns a list of all lines. Iterating with for line in f: is memory-efficient for large files.\n\nWriting: f.write(text) writes a string; f.writelines(list) writes multiple strings. Always include newline characters ('\\n') where needed since write() does not add them automatically.\n\nCommon formats: plain text (.txt), CSV (use Python's csv module for robust parsing), and JSON (use json.load() to read and json.dump() to write structured data). Wrap file operations in try/except blocks to handle missing files, permission errors, and encoding issues gracefully."),
                ("Error Handling and Exceptions", "Errors in Python fall into two categories: syntax errors (caught before execution) and exceptions (runtime errors). Handling exceptions gracefully prevents crashes and enables meaningful recovery.\n\nThe try/except block catches exceptions: try: risky_operation() / except ValueError as e: handle_value_error(e) / except FileNotFoundError: handle_missing_file() / else: run_if_no_exception() / finally: always_runs_for_cleanup().\n\nCommon exceptions include ValueError (wrong type of value provided), TypeError (operation on incompatible types), FileNotFoundError (file does not exist), IndexError (list index out of range), KeyError (dictionary key missing), and ZeroDivisionError.\n\nRaising exceptions: use raise ValueError('Message') to signal errors explicitly. Custom exceptions are created by subclassing Exception, allowing domain-specific error types that communicate intent clearly.\n\nGood error handling logs meaningful messages, provides recovery paths, and avoids bare except: clauses that silently swallow all errors. Clear, specific error messages help users understand what went wrong and help developers diagnose issues during debugging."),
                ("Working with Libraries and pip", "Python's strength comes from its ecosystem of third-party libraries. pip is the standard package manager for installing and managing these packages from PyPI (Python Package Index).\n\nKey pip commands: pip install requests (install), pip install requests==2.28.0 (pin version), pip uninstall requests (remove), pip list (show installed), pip show requests (details), pip freeze > requirements.txt (snapshot), pip install -r requirements.txt (reproduce environment).\n\nVirtual environments isolate project dependencies to prevent version conflicts between projects. Create one with python -m venv venv. Activate with source venv/bin/activate (Mac/Linux) or venv\\Scripts\\activate (Windows). Packages installed inside only affect that project.\n\nPopular libraries by domain: data science (NumPy, Pandas, Matplotlib, seaborn), web development (Flask, Django, FastAPI), machine learning (scikit-learn, TensorFlow, PyTorch), HTTP (requests, httpx), testing (pytest), and database (SQLAlchemy).\n\nBefore adding a package, evaluate its maintenance status, community adoption, and security advisories. A well-chosen set of dependencies reduces complexity, while excessive dependencies increase attack surface and maintenance burden."),
                ("Debugging and Testing in Python", "Debugging identifies and fixes errors in code. Testing verifies that code behaves correctly across inputs and edge cases, preventing regressions as code evolves.\n\nDebugging techniques: print() statements for quick value inspection; the built-in pdb debugger (import pdb; pdb.set_trace() or breakpoint()) for interactive step-through execution; IDE debuggers in VS Code and PyCharm offer breakpoints, watch variables, and call stack inspection.\n\nUnit testing validates individual functions in isolation. pytest is the most popular Python testing framework — tests are simple functions prefixed with test_. assert statements check expected behavior: assert add(2, 3) == 5. pytest provides clear failure messages and useful plugins.\n\nTest-Driven Development (TDD) writes tests before code — define expected behavior first, then implement to pass the tests. This reduces bugs, clarifies requirements, and produces cleaner function signatures.\n\ncode coverage tools (coverage.py, integrated in pytest via pytest-cov) measure what percentage of code is exercised by tests. Aim for high coverage on critical logic. A small suite of meaningful, well-named tests is more valuable than many shallow ones that only test happy paths.")
            }},
            { 4, new List<(string, string)>
            {
                ("JavaScript DOM Manipulation", "The DOM (Document Object Model) represents an HTML page as a tree of objects that JavaScript can read and modify, making pages dynamic and interactive without full page reloads.\n\nSelecting elements: document.getElementById('id'), document.querySelector('.class') (first match), document.querySelectorAll('p') (all matches). Once selected, properties can be read or changed: element.textContent, element.innerHTML, element.style.color, element.classList.add('active').\n\nCreating and inserting elements: document.createElement('div') creates a new node. parent.appendChild(child) or parent.insertBefore(child, ref) add it to the DOM. element.remove() deletes an element.\n\nEvent listeners respond to user actions: element.addEventListener('click', function(e) { ... }). Common events: click, submit, keydown, input, mouseover, DOMContentLoaded. Event delegation attaches one listener to a parent element to handle events from all children, improving performance for dynamic lists.\n\nDOM manipulation performance: batch DOM changes, avoid reading layout properties repeatedly inside loops, and use DocumentFragment for bulk insertions to minimize expensive reflows. Modern frameworks like React abstract direct DOM manipulation using a virtual DOM for efficiency."),
                ("Responsive Web Design", "Responsive design ensures websites adapt to any screen size, from smartphones to wide desktop monitors, using a single codebase.\n\nCSS Flexbox handles one-dimensional layouts. Key properties: display: flex, justify-content (main-axis alignment), align-items (cross-axis alignment), flex-wrap (wrapping behavior), gap (spacing between items). Flexbox is ideal for navigation bars, card rows, and vertically centering content.\n\nCSS Grid handles two-dimensional layouts. grid-template-columns and grid-template-rows define structure. grid-column and grid-row place items precisely. Grid excels at complex page layouts like dashboards and magazine-style designs.\n\nMedia queries apply styles at breakpoints: @media (max-width: 768px) { ... }. Mobile-first design starts with the smallest screen and adds complexity using min-width queries for larger screens. The viewport meta tag (<meta name='viewport' content='width=device-width, initial-scale=1'>) ensures mobile browsers scale correctly.\n\nFluid layouts use relative units: percentages for widths, rem/em for typography, vw/vh for viewport-relative sizing. Images should use max-width: 100% and srcset to serve appropriately sized images to each device. Browser dev tools' responsive mode helps catch layout issues early."),
                ("Web APIs and the Fetch API", "Web APIs allow web applications to communicate with external servers and services over HTTP. REST (Representational State Transfer) is the dominant API design style.\n\nREST APIs use HTTP methods semantically: GET (retrieve data), POST (create), PUT/PATCH (update), DELETE (remove). Responses are typically JSON. HTTP status codes communicate outcomes: 200 (OK), 201 (Created), 400 (Bad Request), 401 (Unauthorized), 404 (Not Found), 500 (Server Error).\n\nThe Fetch API makes HTTP requests from JavaScript: fetch('https://api.example.com/data').then(res => res.json()).then(data => console.log(data)). With async/await: const res = await fetch(url); if (!res.ok) throw new Error(res.status); const data = await res.json().\n\nCORS (Cross-Origin Resource Sharing) is a browser security policy restricting requests to different origins. Servers must include appropriate headers (Access-Control-Allow-Origin) to permit cross-origin requests.\n\nAPI authentication: APIs use API keys (passed in headers or query params), Bearer tokens (OAuth2), or session cookies. Never embed secret keys in client-side JavaScript — use environment variables and backend proxies to keep credentials secure in production."),
                ("Version Control with Git", "Git is the industry-standard distributed version control system for tracking code changes and enabling team collaboration on software projects.\n\nCore concepts: Repository (project tracked by Git), Commit (snapshot with a descriptive message), Branch (independent line of development), Tag (named release point), Remote (server-hosted copy on GitHub, GitLab, or Bitbucket).\n\nEssential workflow: git init (or git clone url), edit files, git add . (stage changes), git commit -m 'message' (save snapshot), git push origin main (upload to remote), git pull (sync from remote), git status (see current state), git log --oneline (view history).\n\nBranching: create branches for features or fixes (git checkout -b feature/login). Develop, commit, push, and open a pull request (PR) for code review before merging to main. This keeps the main branch stable and deployable.\n\nBest practices: write clear, present-tense commit messages ('Add user authentication'), make small focused commits, use .gitignore to exclude build artifacts and secrets, and never commit sensitive credentials. Effective Git usage is a fundamental professional skill for every developer."),
                ("Web Accessibility and Performance", "Web accessibility ensures websites are usable by everyone including people with visual, auditory, motor, and cognitive disabilities. Performance ensures fast, smooth experiences on all devices and connections.\n\nWCAG (Web Content Accessibility Guidelines) principles — Perceivable, Operable, Understandable, Robust — guide accessible design. Practical techniques: provide alt text for images, ensure sufficient color contrast (4.5:1 ratio for body text), label all form inputs, enable keyboard navigation, and manage focus for modals and dynamic content.\n\nSemantic HTML naturally improves accessibility: use button for buttons (not div), heading levels logically (h1 → h2 → h3), and landmark elements (nav, main, footer). Screen readers (NVDA, VoiceOver) rely on semantic structure to navigate pages.\n\nCore Web Vitals — Largest Contentful Paint (LCP), Cumulative Layout Shift (CLS), and Interaction to Next Paint (INP) — measure real-user performance. These affect both user experience and search engine ranking.\n\nPerformance optimization: compress and lazy-load images, minify CSS and JavaScript, enable browser caching, use a CDN for static assets, defer non-critical scripts, and choose efficient web fonts. Tools like Lighthouse, PageSpeed Insights, and axe audit both performance and accessibility simultaneously.")
            }},
            { 5, new List<(string, string)>
            {
                ("Articles and Determiners", "Articles are a subset of determiners — words placed before nouns to indicate specificity, quantity, or possession. Mastering them is essential for natural English.\n\nThe definite article 'the' refers to a specific noun already known to both speaker and listener: 'Please close the door' (a specific, known door). The indefinite articles 'a' and 'an' introduce a non-specific noun for the first time: 'I saw a dog.' Use 'an' before vowel sounds: an apple, an hour (the 'h' is silent), an MBA.\n\nZero article (no article) is used with: uncountable nouns used generally (Water is essential), plural nouns used generally (Dogs are loyal), most proper nouns (She lives in London), and fields of study (He studies medicine).\n\nOther common determiners: demonstratives (this, that, these, those), possessives (my, your, his, her, its, our, their), quantifiers (some, any, many, few, much, little, several), and numbers (one, two, first, second).\n\nArticle mistakes are among the most common errors for speakers of languages without articles (e.g., Russian, Chinese, Japanese). The key is asking: Is this noun specific or general? Countable or uncountable? First mention or already known? These three questions resolve most article choices."),
                ("Modal Verbs", "Modal verbs express ability, possibility, permission, obligation, and other nuances. The main modals are: can, could, may, might, shall, should, will, would, must, and ought to.\n\nAbility: 'can' for present ability (I can swim), 'could' for past ability (I could swim as a child). Permission: 'can' (informal — Can I leave?) vs. 'may' (formal — May I leave?). 'Could' and 'might' express more tentative requests.\n\nObligation and advice: 'must' expresses strong obligation or logical certainty (You must wear a seatbelt / He must be tired). 'Should' gives advice or expectation (You should see a doctor). 'Have to' expresses external obligation (I have to submit this by Friday).\n\nPossibility: 'may' and 'might' express uncertainty (It may/might rain). 'Can't' expresses logical impossibility (She can't be serious — that makes no sense).\n\nModals are followed by the base infinitive (no 'to') and do not change form for third-person singular. Perfect modals (should have done, could have said, must have known) refer to past situations — used for regret, criticism, and past deductions. These are essential in advanced spoken and written English."),
                ("Conditionals", "Conditional sentences describe the relationship between a condition and its result. English has four main conditional types, each expressing a different degree of reality or possibility.\n\nZero conditional: general truths and scientific facts. Both clauses use simple present: 'If you heat water to 100°C, it boils.' The 'if' clause can be replaced with 'when' without changing meaning.\n\nFirst conditional: real, likely future possibilities. Condition: simple present; result: will + infinitive. 'If it rains tomorrow, I will cancel the trip.' Also used with can, may, might, should in the result clause.\n\nSecond conditional: unreal or hypothetical present/future situations. Condition: past simple; result: would + infinitive. 'If I had more time, I would learn to paint.' The past tense does not refer to the past here — it signals unreality.\n\nThird conditional: unreal past situations — regrets and speculations about what could have been. Condition: past perfect; result: would have + past participle. 'If I had studied harder, I would have passed.'\n\nMixed conditionals combine elements: 'If I had saved more money (past), I would be traveling now (present).' Conditionals are fundamental for expressing hypotheses, advice, regrets, and possibilities — mastering them significantly advances fluency."),
                ("Passive Voice", "The passive voice shifts focus from the subject performing an action to the object receiving it. It is widely used in formal, academic, and technical writing.\n\nActive: 'The engineer fixed the bug.' Passive: 'The bug was fixed by the engineer.' The active object becomes the passive subject. The agent ('by the engineer') is optional and often omitted.\n\nForming the passive: appropriate tense of 'be' + past participle. Examples: is written (simple present passive), was written (simple past), has been written (present perfect), will be written (future), is being written (present continuous).\n\nUse the passive when: the doer is unknown ('The window was broken'), the doer is irrelevant ('The package was delivered'), you want to emphasize the action or result rather than who performed it, or in scientific writing to maintain objectivity ('The samples were analyzed').\n\nHowever, overuse of passive in everyday communication makes writing feel impersonal, vague, and difficult to read. The best approach is choosing passive deliberately when it serves clarity or appropriate formality — not defaulting to it out of habit. Active writing is generally more direct and engaging."),
                ("Reported Speech", "Reported (indirect) speech conveys what someone said without quoting them directly. It requires changes to pronouns, tenses, and time expressions.\n\nDirect: She said, 'I am tired.' Reported: She said (that) she was tired. The tense typically backshifts one step: present simple → past simple, present continuous → past continuous, past simple → past perfect, will → would, can → could, may → might.\n\nTime and place expressions shift to reflect the reporter's perspective: now → then, today → that day, tomorrow → the next day/the following day, yesterday → the day before, here → there.\n\nReporting verbs: 'say' does not require an object (He said he was late), while 'tell' requires one (He told me he was late). Other reporting verbs add meaning: admit, deny, explain, promise, suggest, warn, offer, refuse.\n\nReported yes/no questions use 'if' or 'whether': 'Are you coming?' → She asked if I was coming. Wh-questions keep the wh-word and use statement word order: 'Where do you live?' → He asked where I lived. Reported commands use tell/ask + object + infinitive: 'Close the door!' → She told him to close the door.")
            }},
            { 6, new List<(string, string)>
            {
                ("Spanish Pronouns", "Pronouns replace nouns and are central to Spanish grammar. Understanding subject, object, and reflexive pronouns is essential for natural communication.\n\nSubject pronouns: yo (I), tú (you, informal), él / ella / usted (he / she / you formal), nosotros/as (we), vosotros/as (you all, Spain), ellos / ellas / ustedes (they / you all). In Spanish, subject pronouns are often omitted since the verb ending indicates the subject.\n\nDirect object pronouns replace the noun receiving the action: me, te, lo/la, nos, os, los/las. Indirect object pronouns indicate to/for whom: me, te, le, nos, os, les. When both appear together, indirect precedes direct: Te lo doy (I give it to you).\n\nReflexive pronouns (me, te, se, nos, os, se) are used with reflexive verbs, indicating the subject acts on itself: Me llamo Ana (My name is Ana — literally 'I call myself Ana'). Many daily routine verbs are reflexive: levantarse, ducharse, vestirse.\n\nPronoun placement: precede conjugated verbs (Lo veo — I see it), attach to infinitives (Quiero verlo — I want to see it), gerunds (Estoy haciéndolo — I am doing it), and affirmative commands (Dámelo — Give it to me). Mastering placement is key to fluid, natural Spanish."),
                ("Present Tense Conjugation", "Mastering present tense conjugation is the first major milestone in Spanish fluency. Regular verbs follow predictable patterns based on their infinitive ending: -ar, -er, or -ir.\n\n-AR verbs (hablar — to speak): hablo, hablas, habla, hablamos, habláis, hablan. -ER verbs (comer — to eat): como, comes, come, comemos, coméis, comen. -IR verbs (vivir — to live): vivo, vives, vive, vivimos, vivís, viven. Note: -er and -ir share the same endings except in nosotros and vosotros.\n\nHigh-frequency irregular verbs must be memorized: ser (soy, eres, es, somos, sois, son), estar (estoy, estás, está, estamos, estáis, están), tener (tengo, tienes...), ir (voy, vas, va...), hacer (hago, haces...).\n\nStem-changing verbs alter their root vowel in all forms except nosotros/vosotros: querer (e→ie): quiero, quieres, quiere; poder (o→ue): puedo, puedes, puede; pedir (e→i): pido, pides, pide.\n\nThe Spanish present tense covers current actions (Ella trabaja — She is working), habitual actions (Trabajo a las 9 — I work at 9), and near-future intentions (Mañana hablo con él — Tomorrow I'll talk to him). Regular practice with the most common 50 verbs builds automatic recall faster than vocabulary lists."),
                ("Telling Time in Spanish", "Knowing how to tell and ask about time is essential for making appointments, scheduling, and daily communication in Spanish.\n\n¿Qué hora es? means 'What time is it?' The response uses Es la... for one o'clock and Son las... for all others: Es la una (It's one o'clock), Son las tres (It's three o'clock), Son las doce (It's noon/midnight).\n\nMinutes past the hour: Son las cuatro y diez (4:10), Son las cinco y cuarto (5:15 — y cuarto = quarter past), Son las seis y media (6:30 — y media = half past). Minutes before the hour (common in Spain): Son las siete menos veinte (6:40 — seven minus twenty). In Latin America, Son las seis cuarenta is more common.\n\nKey time vocabulary: de la mañana (a.m.), de la tarde (p.m., afternoon), de la noche (evening/night). El mediodía (noon), la medianoche (midnight). En punto means 'exactly' or 'on the dot': Son las tres en punto.\n\n¿A qué hora...? asks 'At what time...?' — A las ocho (At eight o'clock). Schedules in Spanish-speaking countries often use the 24-hour clock in formal contexts. Practicing telling time with real clocks and scheduling conversations accelerates practical fluency."),
                ("Spanish Prepositions", "Prepositions link nouns, pronouns, and phrases to other parts of the sentence, expressing relationships of location, time, direction, cause, and purpose.\n\nCommon prepositions: a (to, at — Voy a la tienda), de (of, from — Soy de México), en (in, on, at — Estoy en casa), con (with — café con leche), sin (without — sin azúcar), por (for, by, through — Gracias por tu ayuda), para (for, in order to — Esto es para ti), sobre (about, on — hablar sobre el tema).\n\nPor vs. para is one of Spanish's most challenging distinctions. Por expresses cause, duration, agent, exchange, and means of travel: Caminé por dos horas; Fue hecho por ella; Gracias por llamar. Para expresses purpose, destination, deadline, and recipient: Estudio para aprender; El vuelo es para Madrid; La carta es para mañana.\n\nMandatory contractions: a + el = al (Voy al mercado), de + el = del (Vengo del trabajo). These always apply and cannot be avoided.\n\nPrepositions of location: encima de (on top of), debajo de (under), delante de (in front of), detrás de (behind), al lado de (beside), entre (between), cerca de (near), lejos de (far from). Learning prepositions through real sentences and spatial practice builds intuitive usage faster than memorizing lists."),
                ("Food and Restaurant Vocabulary", "Food vocabulary and restaurant phrases are among the most useful and culturally rich areas of Spanish, enabling authentic interactions in Spanish-speaking countries.\n\nMeal names: el desayuno (breakfast), el almuerzo (lunch — though timing varies by country), la merienda (afternoon snack), la cena (dinner). Common foods: el pan (bread), la carne (meat), el pollo (chicken), el pescado (fish), las verduras (vegetables), la fruta (fruit), el arroz (rice), los frijoles/judías (beans), el queso (cheese), los huevos (eggs).\n\nRestaurant essentials: Una mesa para dos, por favor (A table for two). ¿Qué me recomienda? (What do you recommend?). Quisiera... / Me gustaría... (I would like...). ¿Tienen opciones vegetarianas? (Do you have vegetarian options?). La cuenta, por favor (The bill, please). ¿Está incluida la propina? (Is the tip included?)\n\nDescribing food: delicioso/a (delicious), rico/a (tasty), picante (spicy), dulce (sweet), salado/a (salty), amargo/a (bitter), fresco/a (fresh), crudo/a (raw), asado/a (grilled/roasted), frito/a (fried).\n\nDietary phrases: Soy vegetariano/a. No como carne. Tengo alergia a los mariscos. These are especially important for health and safety. Learning food vocabulary through authentic menus, recipes, and restaurant visits makes the experience memorable and practical.")
            }},
            { 7, new List<(string, string)>
            {
                ("Network Devices Deep Dive", "Understanding network devices in detail helps design, configure, and troubleshoot network infrastructure effectively at any scale.\n\nHubs broadcast data to all connected ports — all devices share bandwidth and see all traffic. Largely obsolete, replaced by switches. Switches use MAC address tables to forward frames only to the intended destination port, greatly improving efficiency. Managed switches support VLANs, QoS, port mirroring, and STP (Spanning Tree Protocol).\n\nRouters operate at Layer 3, using routing tables to forward IP packets between networks. They define broadcast domain boundaries. Home routers typically combine routing, switching, wireless access point, and NAT in a single device.\n\nWireless Access Points (APs) provide Wi-Fi connectivity, extending wired networks to wireless devices. Enterprise access point systems use controllers or cloud management for central configuration, monitoring, and seamless roaming.\n\nFirewalls filter traffic between network zones based on rules. Proxy servers act as intermediaries between clients and servers — providing caching, content filtering, and anonymity. Load balancers distribute traffic across multiple servers to improve availability and performance."),
                ("Wireless Networking", "Wi-Fi uses radio waves to transmit data without cables, enabling mobility and flexible network design across homes, offices, and public spaces.\n\nWi-Fi standards (IEEE 802.11): 802.11n (Wi-Fi 4, up to 600 Mbps, 2.4/5 GHz), 802.11ac (Wi-Fi 5, up to 3.5 Gbps, 5 GHz), 802.11ax (Wi-Fi 6, up to 9.6 Gbps, improved dense-environment performance), Wi-Fi 6E (adds 6 GHz for less congestion), Wi-Fi 7 (multi-link operation, even higher throughput).\n\nFrequency bands: 2.4 GHz provides better range and wall penetration but is more congested (shared with microwaves, Bluetooth). 5 GHz offers higher speed with less interference but shorter range. Dual-band and tri-band routers support multiple bands simultaneously.\n\nSecurity: WEP (deprecated, trivially cracked), WPA (improved but outdated), WPA2 (widely used with AES), WPA3 (current standard, stronger encryption, forward secrecy). Always use WPA2 or WPA3 with a strong passphrase. Hiding the SSID provides minimal real security.\n\nPlanning Wi-Fi deployments: consider coverage area, number of clients, bandwidth demand, and interference sources. Non-overlapping channels on 2.4 GHz are 1, 6, and 11. Mesh systems use multiple nodes for seamless whole-home or office coverage."),
                ("DNS and DHCP", "DNS (Domain Name System) and DHCP (Dynamic Host Configuration Protocol) are critical infrastructure services that make networks user-friendly and manageable at scale.\n\nDNS translates human-readable domain names (www.example.com) to IP addresses. The hierarchical resolution process: client cache → local resolver → root server → TLD server → authoritative server. Responses are cached using TTL (Time to Live) values to reduce lookup latency.\n\nKey DNS record types: A (domain → IPv4 address), AAAA (domain → IPv6), CNAME (alias pointing to another name), MX (mail server priority and address), TXT (verification data, SPF records), NS (authoritative name server). Misconfigured DNS records cause outages and email delivery failures.\n\nDHCP automates IP address assignment. When a device joins a network, the DORA process runs: Discover → Offer → Request → Acknowledge. The lease includes IP address, subnet mask, default gateway, and DNS servers. DHCP reservations bind specific IPs to MAC addresses for predictable addressing.\n\nTroubleshooting DNS: nslookup, dig, and host query DNS directly. ipconfig /flushdns (Windows) clears cached records. DHCP issues often appear as 169.254.x.x self-assigned addresses, indicating the client received no DHCP response."),
                ("VPNs and Remote Access", "VPNs (Virtual Private Networks) create encrypted tunnels over public networks, enabling secure remote access and protecting data in transit from eavesdropping.\n\nHow a VPN works: the client encrypts traffic before transmission, sends it to the VPN server, which decrypts and forwards it to the destination. From the internet's perspective, traffic originates from the VPN server's IP, masking the user's real address and location.\n\nVPN protocols: OpenVPN (open-source, highly configurable, secure), WireGuard (modern, fast, minimal codebase), IKEv2/IPsec (stable, excellent on mobile with roaming), L2TP/IPsec (older, widely supported but slower). SSL/TLS VPNs work through web browsers, requiring no dedicated client.\n\nSite-to-site VPNs connect entire networks (branch offices to HQ). Remote-access VPNs connect individual devices. Split tunneling routes only corporate traffic through the VPN, reducing bandwidth overhead. Full-tunnel routes all traffic for maximum security.\n\nLimitations: VPNs protect data in transit but do not prevent malware, phishing, or tracking via cookies. Zero trust architectures are increasingly replacing traditional VPNs — verifying identity and device posture for each application access decision rather than granting broad network access after a single authentication."),
                ("Network Monitoring and Troubleshooting", "Network monitoring proactively tracks performance and availability. Systematic troubleshooting quickly identifies and resolves connectivity and performance problems.\n\nMonitoring tools collect metrics: bandwidth utilization, latency, packet loss, interface errors, and device availability. SNMP (Simple Network Management Protocol) and NetFlow expose this data to management platforms (Nagios, PRTG, Zabbix, Grafana). Alerting on thresholds enables proactive response before users notice problems.\n\nCore CLI tools: ping (reachability and round-trip latency), traceroute/tracert (path and per-hop latency), nslookup/dig (DNS resolution), ipconfig/ifconfig (local configuration), netstat/ss (active connections and listening ports), arp -a (ARP table).\n\nOSI-layer troubleshooting methodology: start at Layer 1 (are cables plugged in? are port LEDs lit?), move up through Layer 2 (switch port status, VLANs), Layer 3 (IP addressing, routing), Layer 4 (firewall rules, port accessibility), to Layer 7 (application-level errors). Skipping layers wastes time.\n\nWireshark captures packets for deep analysis. Filters (tcp, http, ip.addr == x.x.x.x) isolate relevant traffic. Reading packet captures reveals protocol errors, authentication issues, and unexpected connection attempts. Systematic documentation of what was changed during troubleshooting prevents compounding problems.")
            }},
            { 8, new List<(string, string)>
            {
                ("HTTP and HTTPS In Depth", "HTTP (Hypertext Transfer Protocol) and HTTPS (HTTP Secure) define how clients and servers exchange data on the web.\n\nHTTP is a stateless request-response protocol. A request includes: method (GET retrieves, POST sends data, PUT/PATCH updates, DELETE removes), URL, headers (Content-Type, Authorization, Cookie, Accept), and optional body. Responses include a status code, headers, and body.\n\nHTTP status codes: 2xx (success — 200 OK, 201 Created, 204 No Content), 3xx (redirects — 301 Moved Permanently, 302 Found), 4xx (client errors — 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found, 429 Too Many Requests), 5xx (server errors — 500 Internal Server Error, 503 Service Unavailable).\n\nHTTPS adds TLS (Transport Layer Security). The TLS handshake: server presents its certificate, client verifies it against a trusted Certificate Authority (CA), session keys are exchanged using asymmetric encryption, then symmetric encryption (AES) secures the session. This prevents eavesdropping, tampering, and impersonation.\n\nHTTP/2 improves performance with multiplexing (concurrent requests over one connection), header compression (HPACK), and server push. HTTP/3 uses QUIC (UDP-based) transport, reducing latency especially on lossy networks. Modern servers should support at least HTTPS with HTTP/2."),
                ("DNS Protocol In Depth", "DNS is a distributed, hierarchical, and highly cached naming system. Understanding it in depth enables faster troubleshooting and better security design.\n\nThe DNS namespace is a tree: root (.) → TLDs (.com, .org, .uk) → second-level domains (example.com) → subdomains (mail.example.com). Each level is served by different authoritative name servers managed independently.\n\nResolution in detail: the stub resolver (on the client) queries the recursive resolver (configured via DHCP or manually, e.g., 8.8.8.8 or 1.1.1.1). The recursive resolver queries root, then TLD, then authoritative servers — caching each answer for the record's TTL to serve future queries faster.\n\nDNS record types in depth: SOA (Start of Authority — zone serial, refresh, retry, expire), PTR (reverse DNS — IP to domain), SRV (service location), CAA (certificate authority authorization). Zone transfers (AXFR) replicate entire zones between primary and secondary DNS servers.\n\nDNS security: cache poisoning injects false records. DNSSEC adds digital signatures to records to prevent tampering. DNS over HTTPS (DoH) and DNS over TLS (DoT) encrypt queries to prevent ISP snooping. Monitoring DNS query logs can reveal compromised devices contacting command-and-control servers."),
                ("DHCP Protocol", "DHCP (Dynamic Host Configuration Protocol) automatically provisions IP addresses and network configuration parameters to devices when they connect to a network.\n\nThe DORA handshake: (1) Discover — client broadcasts searching for any DHCP server. (2) Offer — server responds with an available IP, lease time, subnet mask, gateway, and DNS servers. (3) Request — client formally accepts the offer and requests those parameters. (4) Acknowledge — server confirms the lease is granted.\n\nLease management: leases expire after a configured time (minutes to days). Clients attempt renewal at 50% of lease duration and again at 87.5%. If renewal fails, the client releases the IP. If a DHCP server is unreachable, clients may self-assign 169.254.x.x link-local addresses.\n\nAdvanced DHCP: scopes define available address pools. Exclusions reserve addresses for static assignment. Reservations (by MAC address) ensure a device always receives the same IP — useful for servers, printers, and IoT devices. DHCP relay agents forward client broadcasts across routers to a centralized DHCP server.\n\nSecurity: DHCP snooping on managed switches blocks rogue DHCP servers from distributing malicious configuration. DHCP exhaustion attacks (flooding with fake requests) deplete the address pool — rate limiting and port security mitigate this."),
                ("Email Protocols", "Email communication relies on multiple protocols that work together for sending, receiving, and accessing messages across different servers and clients.\n\nSMTP (Simple Mail Transfer Protocol) sends email from client to server and between mail servers. Modern SMTP uses TLS for encryption and authentication on port 587 (submission) or 465 (SMTPS). Port 25 is reserved for server-to-server relay.\n\nPOP3 (Post Office Protocol 3) downloads email to a client and typically removes it from the server. Simple and suitable for single-device access. Ports: 110 (unencrypted), 995 (SSL/TLS).\n\nIMAP (Internet Message Access Protocol) keeps email on the server and synchronizes across all devices — the standard for modern multi-device email access. Ports: 143 (with STARTTLS), 993 (SSL/TLS). IMAP folders, search, and flags provide a rich client experience.\n\nEmail authentication reduces spam and phishing: SPF (Sender Policy Framework) lists servers authorized to send for a domain in DNS TXT records. DKIM (DomainKeys Identified Mail) cryptographically signs message headers and body. DMARC (Domain-based Message Authentication, Reporting & Conformance) specifies policy (none/quarantine/reject) when SPF or DKIM fails. All three working together significantly reduce email spoofing."),
                ("Network Troubleshooting Tools", "Effective network troubleshooting uses a structured set of command-line and graphical tools that diagnose problems at different OSI layers.\n\nLayer 1–3 tools: ping sends ICMP echo requests to test reachability and measure round-trip time. traceroute (Linux/macOS) / tracert (Windows) maps the packet path and identifies hops with high latency or packet loss. ipconfig /all (Windows) or ip addr, ip route (Linux) show full network configuration.\n\nLayer 3–4 tools: netstat / ss list active connections and listening ports — crucial for identifying unexpected services or connections. arp -a shows the ARP table mapping IP to MAC addresses. curl and wget test HTTP/S connectivity and retrieve headers and content from web servers.\n\nDNS tools: nslookup performs interactive DNS queries. dig provides detailed DNS response information including TTLs and authoritative servers. host is a simpler alternative on Linux/macOS. Testing with specific DNS servers (nslookup domain 8.8.8.8) confirms resolver-specific problems.\n\nDeep analysis: Wireshark captures and decodes all packets passing through a network interface. Filters by IP (ip.addr == 192.168.1.1), protocol (tcp, dns, http), or port (tcp.port == 443) isolate relevant traffic. tcpdump provides command-line packet capture — essential when a GUI is unavailable on remote servers.")
            }},
            { 9, new List<(string, string)>
            {
                ("Feature Engineering", "Feature engineering transforms raw data into informative input variables that improve machine learning model performance. It is often the highest-leverage activity in the ML development cycle.\n\nNumerical features frequently need scaling: StandardScaler centers data at zero with unit variance; MinMaxScaler scales to [0, 1]. Many algorithms (SVM, neural networks, KNN, gradient descent) are sensitive to feature scale and perform poorly without normalization.\n\nCategorical features must be converted to numbers: one-hot encoding creates a binary column per category (suitable for nominal data with few categories); label encoding assigns integers (suitable for ordinal or tree-based models); target encoding replaces categories with the mean target value (effective but risks leakage if applied before splitting).\n\nCreating new features: extract date components (day of week, month, year), compute ratios or products of existing columns, apply log transforms to skewed distributions, and flag missing values as a separate binary column rather than simply imputing.\n\nFeature selection removes noise and irrelevant variables: correlation analysis, recursive feature elimination, and L1 regularization (which zeroes out unimportant features) all reduce overfitting. Good feature engineering often outperforms model tuning — a simple model on excellent features usually beats a complex model on poor ones."),
                ("Unsupervised Learning", "Unsupervised learning discovers structure in data without predefined labels. The algorithm finds patterns, groupings, or compressed representations on its own.\n\nClustering groups similar data points. K-Means assigns points to k clusters by minimizing within-cluster distances. Initialization matters — k-means++ improves convergence. DBSCAN finds clusters of arbitrary shape based on density, handling noise well and requiring no preset k. Hierarchical clustering builds a dendrogram, useful for visualizing nested structure.\n\nDimensionality reduction compresses data into fewer dimensions while preserving variance or structure. PCA (Principal Component Analysis) finds axes of maximum variance — widely used for visualization and preprocessing. t-SNE and UMAP project high-dimensional data into 2D or 3D for visual exploration, revealing cluster structure.\n\nAnomaly detection identifies outliers that deviate significantly from normal patterns. Isolation Forest, One-Class SVM, and autoencoders are popular approaches. Applications include fraud detection, network intrusion detection, and manufacturing quality control.\n\nEvaluating unsupervised models is harder than supervised: there are no ground-truth labels. Metrics like silhouette score (cluster cohesion/separation) and domain expert review help assess quality. Visualization is often the most important evaluation tool."),
                ("Reinforcement Learning", "Reinforcement learning (RL) trains an agent to make sequential decisions by interacting with an environment and receiving scalar reward signals, without labeled training examples.\n\nCore components: Agent (the learner), Environment (what it interacts with), State (the current situation), Action (the agent's choice), Reward (immediate feedback), Policy (the strategy mapping states to actions), and Value function (expected cumulative future reward).\n\nExploration vs. exploitation: the agent must balance trying new actions (exploration) to find better strategies and exploiting known good actions. ε-greedy policy takes random actions with probability ε and greedy actions otherwise. Decaying ε over time shifts from exploration to exploitation as the agent learns.\n\nKey algorithms: Q-Learning learns action-value (Q-value) tables for discrete state spaces. Deep Q-Networks (DQN) use neural networks to approximate Q-values for high-dimensional states (pixel inputs for Atari games). Policy gradient methods (REINFORCE, PPO) directly optimize the policy. Actor-Critic methods combine value estimation and policy optimization.\n\nApplications: game playing (AlphaGo, OpenAI Five, AlphaStar), robotics (dexterous manipulation), autonomous driving, and recommendation systems. RL is powerful but sample-inefficient — many environments require millions of interactions to converge. Reward shaping and curriculum learning help."),
                ("Model Deployment", "Training an ML model is only the beginning. Deploying it to production means serving real requests reliably, efficiently, and at scale while continuously monitoring quality.\n\nDeployment approaches: export models to standard formats (ONNX for cross-framework compatibility, pickle/joblib for scikit-learn, TensorFlow SavedModel, PyTorch TorchScript). Wrap them in a REST API using FastAPI or Flask, containerize with Docker, and deploy to cloud infrastructure (AWS SageMaker, Google Vertex AI, Azure ML) or Kubernetes.\n\nModel serving considerations: latency (time to first prediction), throughput (predictions per second), memory footprint, and graceful handling of malformed inputs. Versioning models enables rollback when new versions underperform.\n\nMonitoring in production: track prediction latency, error rates, input feature distributions (data drift), and output distributions (concept drift). Alerting on significant drift triggers investigation and potential retraining.\n\nMLOps practices apply DevOps principles to ML: version-controlled data and model artifacts, reproducible experiment tracking (MLflow, Weights & Biases), automated retraining pipelines, CI/CD for model evaluation and deployment. A/B testing compares model versions on live traffic. Production reliability requires engineering rigor equal to model development quality."),
                ("Ethics in AI", "As AI systems become more capable and widely deployed, ethical considerations become central to responsible development, deployment, and governance.\n\nBias and fairness: models trained on biased data perpetuate and can amplify societal inequities. Examples include facial recognition with higher error rates for darker skin tones, and hiring algorithms that disadvantage women. Bias enters through training data, feature selection, proxy variables, and optimization objectives. Fairness metrics attempt to measure and reduce it, but different definitions of fairness are often mathematically incompatible.\n\nTransparency and explainability: many powerful models are 'black boxes.' SHAP (SHapley Additive exPlanations) and LIME (Local Interpretable Model-agnostic Explanations) provide local explanations of individual predictions. Explainability is critical in high-stakes domains — healthcare, criminal justice, credit scoring — where individuals must be able to understand and challenge automated decisions.\n\nPrivacy: training on personal data raises consent and re-identification risks. Differential privacy adds mathematically bounded noise to training, limiting what individual data can be inferred. Federated learning trains models across distributed devices without centralizing raw data.\n\nGovernance: the EU AI Act categorizes AI systems by risk level and imposes corresponding requirements. Responsible AI development requires diverse teams, clear accountability, external audits, user redress mechanisms, and ongoing monitoring for harmful outcomes after deployment.")
            }},
            { 10, new List<(string, string)>
            {
                ("Convolutional Neural Networks", "Convolutional Neural Networks (CNNs) are designed for processing grid-structured data — primarily images — by automatically learning hierarchies of spatial features through convolution operations.\n\nThe convolution operation slides a learnable filter (kernel) across the input, computing an element-wise dot product at each position to produce a feature map. Each filter detects a specific pattern (edge, color gradient, texture). Multiple filters applied to the same input produce a multi-channel feature map stack.\n\nKey layer types: Convolutional layers apply filters and learn feature detectors. Pooling layers (max pooling, average pooling) downsample feature maps, reducing spatial dimensions and computation while providing translation invariance. Batch normalization stabilizes training and allows higher learning rates. Fully connected layers at the end combine learned features for the final classification.\n\nEvolutionary architectures: LeNet (1990s, handwriting), AlexNet (2012, ImageNet breakthrough), VGGNet (deep, uniform 3×3 filters), ResNet (residual skip connections enabling 100+ layer networks), EfficientNet (compound scaling of width, depth, and resolution). Each addressed different challenges in training deep networks.\n\nBeyond classification: object detection (YOLO, SSD, Faster R-CNN), semantic segmentation, medical image analysis (tumor detection, retinal scanning), and video understanding all build on CNN foundations. Transfer learning from ImageNet-pretrained CNNs is effective even with very small task-specific datasets."),
                ("Recurrent Neural Networks and LSTMs", "Recurrent Neural Networks (RNNs) process sequential data by maintaining a hidden state that carries context from previous time steps, making them naturally suited for time series, text, and audio.\n\nIn a standard RNN, the hidden state h_t depends on the current input x_t and the previous hidden state h_{t-1}: h_t = tanh(W_h·h_{t-1} + W_x·x_t). This recurrence allows modeling temporal dependencies — but vanilla RNNs suffer from the vanishing gradient problem, where information from many steps back fades during training.\n\nLSTMs (Long Short-Term Memory) solve this with a cell state and three gates: the forget gate (decide what to discard from memory), the input gate (decide what new information to add), and the output gate (decide what to pass to the next hidden state). Gates are parameterized sigmoid-activated layers, enabling the network to learn what to remember or forget.\n\nGRUs (Gated Recurrent Units) simplify LSTMs into two gates (update and reset), achieving comparable performance with fewer parameters. Bidirectional RNNs process sequences in both directions, capturing context from past and future tokens — useful in NLP tasks where full sequence context improves predictions.\n\nDespite Transformers dominating NLP, RNNs and LSTMs remain relevant in streaming audio, lightweight on-device sequence models, time series forecasting, and real-time control systems where Transformer complexity is prohibitive."),
                ("Transformers and Attention Mechanisms", "The Transformer architecture, introduced in 'Attention Is All You Need' (Vaswani et al., 2017), replaced sequential recurrence with parallel self-attention, enabling efficient training on massive datasets and capturing long-range dependencies that RNNs struggle with.\n\nSelf-attention computes relationships between all token pairs simultaneously. For each token, three vectors are computed — Query (Q), Key (K), Value (V) — using learned weight matrices. Attention scores are computed as scaled dot products of Q and K, softmaxed, then used to weight V: Attention(Q,K,V) = softmax(QK^T / √d_k)V. High scores indicate high relevance between token pairs.\n\nMulti-head attention runs several parallel attention mechanisms with different weight matrices, capturing different relationship types (syntactic, semantic, coreference). Positional encodings add sequence order information since self-attention has no inherent notion of position.\n\nThe Transformer encoder (BERT-style) processes input bidirectionally — ideal for understanding tasks like classification and question answering. The decoder (GPT-style) generates text autoregressively — ideal for language generation. Encoder-decoder models (T5, BART) handle translation and summarization.\n\nScaling Transformers with more parameters, data, and compute produces emergent capabilities (chain-of-thought reasoning, in-context learning, code generation). GPT-4, Claude, Gemini, and Llama are all Transformer-based. The architecture now extends beyond text to vision (ViT), audio, and multimodal AI."),
                ("Transfer Learning", "Transfer learning reuses knowledge learned from one task or domain to improve performance on a different but related task — dramatically reducing the data and compute required for new applications.\n\nIn traditional ML, models train from scratch for each task. Transfer learning leverages the fact that low-level features (edges, textures for images; syntax, common phrases for text) generalize across domains. A model pretrained on a large dataset encodes this reusable knowledge in its weights.\n\nApproaches: Feature extraction freezes all pretrained layers and only trains a new task-specific head — fast and effective when source and target domains are similar. Fine-tuning unfreezes some or all pretrained layers and continues training on task data with a small learning rate — better when domains differ significantly. Prompt-based transfer uses LLMs for zero-shot or few-shot learning by framing tasks as natural language prompts.\n\nIn vision: ImageNet-pretrained CNNs transfer effectively to medical imaging, satellite imagery, and industrial defect detection despite domain differences. Earlier layers capture universal edge and texture features; only later layers need significant adaptation.\n\nIn NLP: BERT, RoBERTa, GPT, and T5 are fine-tuned for sentiment analysis, named entity recognition, question answering, and code generation. Pre-training on massive corpora gives models broad linguistic knowledge that fine-tuning on task-specific data then specializes. Transfer learning has democratized AI by reducing the barrier of data requirements."),
                ("AI Safety and Alignment", "AI safety and alignment research addresses how to ensure increasingly capable AI systems behave reliably, safely, and in accordance with human values — a challenge that grows in importance as AI capabilities scale.\n\nThe alignment problem: an AI system optimizing for a specified objective may find unexpected and harmful ways to maximize that objective if the objective is not perfectly specified. Small misalignments between stated objectives and intended behavior can produce catastrophic outcomes at scale.\n\nCurrent practical safety challenges: hallucination (models generating confident but false information), prompt injection (malicious instructions overriding intended behavior), jailbreaking (circumventing safety guardrails), reward hacking (gaming evaluation metrics), and sycophancy (agreeing with users rather than providing truthful responses).\n\nAlignment techniques: Reinforcement Learning from Human Feedback (RLHF) trains models to produce outputs preferred by human raters. Constitutional AI (Anthropic) uses a set of principles to guide self-critique and revision. Interpretability research (mechanistic interpretability, probing classifiers) aims to understand what models learn internally to detect misaligned objectives.\n\nGovernance and policy: the EU AI Act, US AI Executive Order, and voluntary commitments by AI labs represent early regulatory efforts. International coordination on safety standards, third-party auditing, and incident reporting are considered essential for responsible scaling. AI safety is a research field with growing academic and industrial investment.")
            }},
            { 11, new List<(string, string)>
            {
                ("Email Etiquette", "Email remains one of the most important professional communication channels. Good email etiquette improves clarity, builds relationships, and prevents misunderstandings that waste time.\n\nSubject lines should be specific and actionable: 'Q3 Budget Review — Decision Needed by Friday' is far more useful than 'Question' or 'Hi.' A clear subject helps recipients prioritize and lets everyone find threads later by search.\n\nMessage structure: greet appropriately (Hi [Name], / Dear Mr. / Ms. [Surname] for formal contexts), state your purpose in the first sentence, provide necessary context concisely, state clearly what you need or are sharing, and close with a polite sign-off (Best regards, Thanks, Sincerely).\n\nTone and clarity: email lacks nonverbal cues — reread before sending to check how tone lands. Avoid ALL CAPS (implies shouting), use exclamation marks sparingly, and be careful with sarcasm and humor that may not translate across cultures or relationships.\n\nCC, BCC, and Reply All: CC people who need to be informed but aren't the primary audience. BCC protects recipient privacy in mass emails or when forwarding sensitive threads. Use Reply All only when all recipients genuinely need your response — overuse creates inbox overload and frustration. A thoughtful email culture respects everyone's time and attention."),
                ("Giving and Receiving Feedback", "Feedback is essential for individual and team growth. Both giving it effectively and receiving it gracefully are learnable skills that, practiced consistently, strengthen professional performance and relationships.\n\nEffective feedback is specific, timely, and actionable. Vague feedback ('Good job' or 'That could be better') helps no one. Specific feedback describes observable behavior and its impact: 'The executive summary led with data instead of the recommendation — leading with the conclusion would make it easier for leadership to act quickly.'\n\nThe SBI model: Situation (describe when and where), Behavior (observable, specific action — not personality or intent), Impact (the effect on you, the team, or the project). Keeping to SBI keeps feedback objective and avoids triggering defensiveness.\n\nDelivering difficult feedback: choose a private setting, stay focused on behavior and outcomes (not character), use 'I' statements, listen to the response, and agree on next steps. The goal is improvement, not blame.\n\nReceiving feedback: listen without interrupting or defending. Ask clarifying questions to understand fully. Separate your emotional reaction from the substantive information — even poorly delivered feedback may contain valuable insight. Thank the giver. Reflect before deciding what to act on. Leaders who openly receive feedback model the behavior they want from their teams."),
                ("Conflict Resolution", "Conflict in professional settings is inevitable. Handled constructively, it strengthens relationships and surfaces better solutions. Handled poorly, it erodes trust, morale, and productivity.\n\nCommon conflict sources: differing values or goals, unclear role boundaries, resource competition, communication breakdowns, unmet expectations, and accumulated unaddressed minor issues. Early, direct addressing is almost always better than avoidance — small conflicts become larger and more costly when ignored.\n\nThomas-Kilmann Conflict Mode Instrument identifies five approaches: Competing (assertive, uncooperative — wins but damages relationships), Collaborating (assertive, cooperative — best outcomes but time-intensive), Compromising (both parties give up something), Avoiding (delay or withdrawal — sometimes appropriate, often costly), and Accommodating (giving in to maintain harmony).\n\nCollaborative resolution steps: create a calm, private setting; each party states their perspective without interruption; identify underlying interests (not just stated positions); jointly brainstorm solutions; agree on specific action steps and follow-up accountability.\n\nMediation brings in a neutral facilitator when direct resolution fails. Emotional self-regulation is critical throughout — taking a brief break before responding when emotions spike prevents escalation and keeps the focus on resolution rather than winning."),
                ("Cross-Cultural Communication", "In globalized workplaces, effective communication across cultural boundaries is an essential professional competency. Cultural differences profoundly shape how people communicate, interpret messages, and build relationships.\n\nHigh-context cultures (Japan, China, many Arab and Latin American cultures) communicate indirectly — relying heavily on shared context, relationships, and nonverbal cues. Low-context cultures (US, Germany, Scandinavia, Australia) communicate directly — meaning is explicit in words, and ambiguity is unwelcome.\n\nHofstede's cultural dimensions: Power Distance (acceptance of hierarchical inequality), Individualism vs. Collectivism, Uncertainty Avoidance (tolerance for ambiguity), Long vs. Short-term Orientation, and Indulgence vs. Restraint. These dimensions explain many cross-cultural communication breakdowns.\n\nPractical communication adaptations: avoid idioms and slang that don't translate, speak clearly and at moderate pace, confirm understanding without making people feel tested, be patient with different conversational pacing, and show genuine curiosity about others' cultural perspectives rather than assuming your approach is the default.\n\nCultural Intelligence (CQ) — awareness of your own assumptions, knowledge of other cultures, and ability to adapt — is trainable through exposure, reflection, and deliberate learning. Teams with high CQ consistently outperform homogeneous groups on creative problem-solving and innovation."),
                ("Negotiation Skills", "Negotiation is the process of reaching a mutually acceptable agreement when parties have different interests. Strong negotiation skills are valuable in business, salary discussions, conflict resolution, project scoping, and everyday professional interactions.\n\nPositional vs. interest-based negotiation: positional negotiation anchors on fixed positions ('I want X, you want Y') and often results in impasse or lose-lose compromises. Interest-based (principled) negotiation, from Getting to Yes (Fisher & Ury), focuses on underlying needs — why each party wants what they want. Multiple positions may satisfy the same interest, opening creative solution space.\n\nBATNA (Best Alternative to a Negotiated Agreement): knowing your fallback option if negotiation fails is the most important source of negotiating power. A strong BATNA lets you walk away from bad deals. Before negotiating, always clarify your own BATNA and try to understand the other party's.\n\nKey tactics: anchoring (the first offer sets the psychological range — make a well-reasoned first move), active listening (understanding the other's perspective reveals their interests and constraints), creating value before claiming it (find mutual-gain solutions first, then divide them), and trading concessions strategically (give less valuable things to get more important ones).\n\nAfter agreement: document outcomes in writing, assign responsibilities and deadlines, and follow through on commitments. Sustainable agreements preserve relationships and set the tone for future negotiations with the same parties.")
            }},
            { 12, new List<(string, string)>
            {
                ("Building High-Performing Teams", "High-performing teams consistently deliver results, adapt to challenges, and grow their members. They don't form by accident — they require intentional leadership and sustained attention to team health.\n\nPsychological safety — team members feeling safe to speak up, disagree, ask questions, and admit mistakes without fear of punishment — is the single strongest predictor of team effectiveness. Google's Project Aristotle found it mattered more than any combination of team composition.\n\nClarity reduces friction: every member should understand the team's goals, their own role, how success is measured, and how decisions are made. Ambiguity about responsibility creates gaps, duplicate effort, and resentment.\n\nDiversity of thought, background, and experience improves problem-solving and innovation quality when leaders create genuinely inclusive conditions. Psychological safety enables diversity of thought to actually surface — without it, diverse teams may still self-censor.\n\nLeader actions that build team performance: regular 1:1 check-ins to understand individual needs and blockers, recognizing contributions specifically and publicly, running retrospectives after milestones, removing organizational obstacles the team cannot clear themselves, and modeling the trust and vulnerability you want to see. High-performing teams are built over months, not days."),
                ("Delegation and Empowerment", "Effective delegation is one of the highest-leverage leadership behaviors. Done well, it develops team members, multiplies a leader's impact, and builds organizational capacity. Done poorly, it abdicates responsibility or micromanages.\n\nCommon delegation failures: not delegating because 'it's faster to do it myself' (true short-term, destructive long-term), over-delegating without providing context or support, micromanaging after delegating (undermining the person and defeating the purpose), or assigning tasks without the authority to make relevant decisions.\n\nEffective delegation: select the right person based on current skill, growth potential, and workload. Clearly communicate the goal and success criteria — not necessarily the method. Provide the necessary authority, context, and resources. Agree on check-in points. Let them own the outcome and make their own decisions within the defined boundaries.\n\nEmpowerment is broader than task delegation — it means giving people genuine decision-making authority within defined boundaries. Empowered employees take more initiative, are more engaged, and feel genuine ownership over outcomes.\n\nBuilding trust through delegation: start with lower-stakes tasks, provide clear feedback, increase responsibility as confidence and competence are demonstrated. Trust builds incrementally through repeated successful delegation — it cannot be manufactured through proclamation alone."),
                ("Performance Management", "Performance management is the ongoing process of setting clear expectations, providing feedback, developing employees, and recognizing contributions to achieve organizational goals.\n\nGoal-setting with SMART criteria: Specific (what exactly will be accomplished?), Measurable (how will progress be tracked?), Achievable (realistic given resources?), Relevant (aligned with team and organizational goals?), Time-bound (what is the deadline?). SMART goals give both manager and employee a shared, unambiguous definition of success.\n\nContinuous feedback is more effective than annual reviews alone. Regular 1:1 check-ins create a continuous improvement loop — the formal annual review should document and summarize what has already been discussed, not deliver surprises.\n\nAddressing underperformance early and directly is essential. A good Performance Improvement Plan (PIP) defines the performance gap clearly, specifies expected improvement, outlines the support provided, sets a timeline, and clarifies consequences. PIPs should be improvement-focused, not merely documentation for dismissal.\n\nRecognizing strong performance drives retention and motivation. Recognition should be timely (close to the achievement), specific (naming what was done well), and appropriate to individual preferences (some prefer public recognition, others private). Top performers who feel unrecognized disengage and leave — performance management is as much about retaining excellent people as managing poor performers."),
                ("Change Management", "Organizations change constantly — in structure, process, technology, and strategy. Change management is the structured discipline of guiding people through transitions with minimal disruption and maximum adoption.\n\nKotter's 8-Step Model: Create urgency (why now?) → Build a guiding coalition (who will lead?) → Form a strategic vision (where are we going?) → Enlist a volunteer army (who will help?) → Enable action by removing barriers → Generate short-term wins to build momentum → Sustain acceleration → Institute change in culture.\n\nPeople resist change for understandable reasons: fear of the unknown, loss of control, extra workload, skill gaps, lack of trust in leadership, and past experiences of change that failed or was handled poorly. Resistance is information — it often signals unaddressed concerns worth exploring.\n\nThe ADKAR model focuses on individual transitions: Awareness (why change?), Desire (personal motivation to support it), Knowledge (how to change?), Ability (can they actually do it?), Reinforcement (what sustains the change over time?). Leaders must address each element for every affected person.\n\nCritical success factors: senior leadership visibly sponsoring the change, involving affected people in solution design, communicating repeatedly across multiple channels, celebrating early wins, and addressing setbacks transparently. Change that isn't embedded in systems, incentives, and culture reverts — sustainment requires as much attention as launch."),
                ("Strategic Thinking", "Strategic thinking is the capacity to see the big picture, understand context and trends, anticipate future challenges, and align current decisions with long-term goals — one of the most differentiating leadership capabilities.\n\nStrategic thinkers ask fundamentally different questions: not just 'How do we do this?' but 'Why are we doing this? What are we choosing not to do? What assumptions are we making? What will the environment look like in three years?'\n\nAnalysis frameworks: SWOT (Strengths, Weaknesses, Opportunities, Threats) assesses internal and external factors. Porter's Five Forces analyzes competitive dynamics. PESTLE (Political, Economic, Social, Technological, Legal, Environmental) scans the macro environment. Scenario planning develops responses to multiple plausible futures.\n\nOKRs (Objectives and Key Results) connect strategic direction to team execution. The objective sets direction ('Become the most trusted provider in the market'), key results measure progress with specific, time-bound metrics. Regular OKR reviews maintain the line-of-sight from daily work to strategic goals.\n\nDeveloping strategic thinking in yourself and teams: protect time for reflection (strategy requires stepping back from the urgent), read broadly beyond your field, analyze decisions in retrospect to extract lessons, seek cross-functional exposure to understand the full business system, and create forums where questioning strategic assumptions is welcomed and expected.")
            }},
            { 13, new List<(string, string)>
            {
                ("Peripherals and Connectivity", "Peripherals expand what a computer can do. Understanding connectivity standards helps select compatible, high-performance devices.\n\nUSB is the dominant peripheral interface. Standards: USB 2.0 (480 Mbps), USB 3.2 Gen 1 (5 Gbps), USB 3.2 Gen 2 (10 Gbps), USB 3.2 Gen 2×2 (20 Gbps), USB4 Gen 3 (40 Gbps). USB-C is a reversible connector shape that can carry USB data, video (DisplayPort/HDMI Alt Mode), and Power Delivery over a single cable.\n\nThunderbolt (Intel): Thunderbolt 4 provides 40 Gbps, supports dual 4K displays, daisy-chaining up to 6 devices, and 100W charging — all over USB-C. Thunderbolt 5 reaches 120 Gbps for demanding workflows. Display connections: HDMI 2.1 supports 4K@144Hz and 8K; DisplayPort 2.1 supports 8K and 16K.\n\nBluetooth connects wireless peripherals (keyboards, mice, headphones, gamepads) with low power consumption. Versions: Bluetooth 5.x offers greater range and speed. Wi-Fi adapters, SD card readers, audio interfaces, and Ethernet ports serve specialized connectivity needs.\n\nSelecting peripherals: match the interface to the required bandwidth (4K streaming over USB-C needs Thunderbolt or USB 3.2 Gen 2), check power delivery requirements, and consider latency-sensitive use cases (wired mice and keyboards have lower latency than Bluetooth — important for professional gaming and precision creative work)."),
                ("PC Building Process", "Building a PC from components offers customization, cost efficiency, and deep hardware understanding. Following a systematic process avoids mistakes.\n\nComponent selection and compatibility: CPU socket must match motherboard (Intel LGA 1700 / AMD AM5 for current gen). RAM type (DDR4/DDR5) and speed must be supported by the motherboard. PSU wattage must cover total system TDP plus headroom (25–30% overhead). GPU physical size must fit the case, and PCIe power connectors must match.\n\nAssembly sequence: install CPU into motherboard socket (align notches/arrows, zero-insertion-force — do not force). Apply a pea-sized drop of thermal paste on the CPU IHS. Mount the cooler. Install RAM in the recommended slots (check manual for dual-channel). Mount PSU in case. Install motherboard. Connect front-panel headers, case fans, and power connectors. Mount storage (NVMe into M.2 slot, SATA with data and power cables). Install GPU in the primary PCIe x16 slot.\n\nFirst power-on: do a bench test before closing the case — breadboarding allows quick reseating if issues arise. Enter BIOS/UEFI, verify all components are detected, enable XMP/EXPO for rated RAM speed, and set boot order. Install OS from bootable USB drive.\n\nAnti-static precautions (touch grounded metal or use ESD wrist strap) protect components from static damage. Cable management improves airflow, aesthetics, and future serviceability. Building your own PC is a rewarding skill that builds lasting hardware intuition."),
                ("Overclocking and Tuning", "Overclocking extracts additional performance from hardware by running it above manufacturer-rated speeds. Understanding thermal and stability trade-offs is essential before attempting it.\n\nCPU overclocking requires an unlocked processor (Intel 'K' series, AMD Ryzen non-G/non-GE on X-series boards). Increase the CPU multiplier in BIOS incrementally (100–200 MHz steps), test stability with Prime95 or Cinebench stress tests, and monitor temperatures with HWInfo64. Increasing CPU core voltage stabilizes higher clocks but significantly increases heat — high-quality coolers are essential.\n\nRAM overclocking is simpler and lower-risk: enable the XMP (Intel) or EXPO (AMD) BIOS profile to activate the memory kit's rated speed. Kits often ship at default 4800 MHz but are rated for 6000+ MHz with a profile. Always run MemTest86 after enabling XMP to verify stability.\n\nGPU overclocking uses MSI Afterburner or EVGA Precision X1. Increase core clock offset (+50–150 MHz), raise power limit to the maximum allowed, and stress-test with FurMark or 3DMark. Memory overclocking (GDDR6X/7) can meaningfully improve VRAM-limited performance.\n\nRisks: higher voltages reduce component longevity over years of sustained use. Aggressive overclocking on low-quality coolers risks thermal damage. Always start conservatively — even a 10–15% CPU overclock provides meaningful real-world gains in CPU-bound workloads."),
                ("PC Troubleshooting", "Systematic PC troubleshooting efficiently isolates hardware, driver, and software issues without unnecessary component replacements.\n\nNo power or no POST: verify PSU is switched on and the power cable is seated. Test with a different outlet. Check the power button header is connected to the correct motherboard pins. Listen for POST beep codes — the pattern indicates the failing component (long beeps usually indicate RAM issues; consult the motherboard manual for specific codes). Modern boards use a Q-LED or debug display showing numerical POST codes.\n\nNo display with the system running: verify the monitor is connected to the GPU (not the motherboard's video out if a discrete GPU is installed). Try reseating the GPU in the PCIe slot. Test with one RAM stick in the first slot. Try a known-working display and cable.\n\nBlue Screen of Death (BSOD): note the stop code (MEMORY_MANAGEMENT points to RAM; CRITICAL_PROCESS_DIED often indicates driver corruption). Run Windows Memory Diagnostic or MemTest86 overnight. Check Device Manager for exclamation marks. Review Event Viewer (System and Application logs) for critical errors coinciding with crashes.\n\nPerformance problems: use Task Manager to identify CPU, RAM, disk, or GPU bottlenecks. Check drive health with CrystalDiskInfo (SSD wear, HDD reallocated sectors). Verify temperatures are within normal ranges (CPU < 90°C under load, GPU < 85°C). Scan for malware. Ensure there is at least 10–15% free SSD space — SSDs degrade in write performance when nearly full."),
                ("Laptop vs Desktop", "Choosing between a laptop and a desktop involves balancing portability, performance, upgradability, repairability, and total cost over time.\n\nDesktops offer the best performance per dollar. Larger cases allow better cooling solutions, enabling sustained higher-performance workloads without throttling. Standard ATX components are easily upgraded: add RAM, swap the GPU, replace storage, or upgrade the CPU within the same socket generation. Desktops are ideal for gaming, video editing, 3D rendering, software development workstations, and any permanently located use case.\n\nLaptops provide portability — work from anywhere, with battery backup protecting against power outages. Thin-and-light ultrabooks (MacBook Air, Dell XPS 13, Lenovo ThinkPad X series) prioritize battery life and weight. Gaming laptops (ASUS ROG, Razer Blade, MSI) bring discrete GPU performance in portable form — at higher cost, more heat, and shorter battery life.\n\nUpgradeability gap: most modern laptops solder RAM and SSD onto the board, making upgrades impossible or very difficult. Some models (Framework, certain ThinkPads) are designed for repairability. Buying the right configuration from day one is more important for a laptop than for a desktop.\n\nHybrid setup for professionals: a portable laptop paired with an external monitor, keyboard, mouse, and docking station at a fixed desk delivers full productivity at the desk and mobility when needed. For most knowledge workers, this combination provides the best of both form factors without the maintenance overhead of two full computers.")
            }},
            { 14, new List<(string, string)>
            {
                ("Cloud Storage Services", "Cloud storage saves data on remote servers managed by third-party providers and accessed over the internet, offering scalability, accessibility, and reduced hardware management burden.\n\nConsumer cloud services: Google Drive (15 GB free, tight Google Workspace integration), Apple iCloud (5 GB free, seamless Apple ecosystem sync), Microsoft OneDrive (5 GB free, integrated with Windows and Office 365), Dropbox (2 GB free, strong third-party app integrations). Business tiers add admin controls, audit logs, compliance features, and larger storage quotas.\n\nEnterprise object storage: Amazon S3, Azure Blob Storage, and Google Cloud Storage store unstructured data at virtually unlimited scale. Pricing is per GB stored plus per-GB-transferred. Durability guarantees of 99.999999999% (11 nines) are achieved through redundant replication across multiple availability zones and regions.\n\nTradeoffs: cloud storage eliminates on-premises hardware costs but introduces ongoing subscription expenses, dependency on internet connectivity, and data sovereignty considerations (regulations may require data to remain within specific countries or jurisdictions).\n\nSecurity considerations: enable MFA on all cloud storage accounts. Audit share permissions regularly — publicly shared links are a major data leak vector. For maximum privacy, use client-side encryption (Cryptomator, Boxcryptor) to ensure files are encrypted before upload and only you hold the keys. Review provider SLAs, retention policies, and compliance certifications relevant to your industry."),
                ("Network Attached Storage", "NAS (Network Attached Storage) is a dedicated storage device connected to a local network, providing centralized file access and sharing for multiple users and devices.\n\nNAS hardware: a compact device with one or more drive bays filled with standard HDD or SSD drives. An embedded OS manages file services, RAID arrays, and additional applications. Popular brands: Synology (DSM OS, excellent UI), QNAP (QTS OS, feature-rich), Western Digital My Cloud, and DIY builds running TrueNAS or OpenMediaVault.\n\nFile sharing protocols: SMB/CIFS (Windows and cross-platform file sharing), NFS (Linux/Unix), AFP (legacy macOS). Most NAS devices support all three simultaneously. iSCSI can present NAS volumes as block devices for VMs and databases.\n\nCommon home and SMB use cases: media server (Plex running on NAS streams video to TVs and devices), personal cloud (Synology Drive or Nextcloud replacing commercial cloud storage), automated backups (macOS Time Machine, Windows Backup, rsync from Linux), and photo archiving.\n\nNAS vs. cloud: NAS provides faster local access speeds (Gigabit Ethernet or faster), one-time hardware cost, and full data sovereignty. However, it requires physical maintenance, has no inherent off-site backup (though most modern NAS systems support hybrid cloud replication), and is vulnerable to local disasters without a remote copy."),
                ("Storage Area Networks", "A SAN (Storage Area Network) is a dedicated high-speed network providing servers with block-level access to shared storage arrays. It is the foundation of enterprise storage infrastructure.\n\nBlock-level vs. file-level access: NAS provides file-level sharing (the NAS manages the file system). A SAN provides raw block access — the connected server mounts the storage and manages its own file system, as if the drive were directly attached. This is essential for databases, virtual machine datastores, and high-transaction workloads requiring maximum IOPS.\n\nSAN technologies: Fibre Channel (FC) uses dedicated FC switches and host bus adapters (HBAs), providing deterministic low-latency performance — the traditional choice for mission-critical enterprise storage. iSCSI transports SCSI commands over IP, using standard Ethernet — lower cost but potentially higher latency. NVMe over Fabrics (NVMe-oF) extends NVMe performance over Ethernet or InfiniBand for the fastest storage access.\n\nEnterprise features: thin provisioning allocates physical space only as written (conserving disk while presenting larger logical volumes). Snapshots capture point-in-time storage states for rapid recovery. Synchronous and asynchronous replication provides disaster recovery.\n\nWhen to use a SAN: large-scale virtualization (shared datastore for hundreds of VMs), high-performance database workloads (Oracle RAC, SQL Server AlwaysOn), and mission-critical applications where storage performance and availability are paramount."),
                ("Data Deduplication and Compression", "Deduplication and compression reduce the physical storage consumed by data, lowering hardware costs and improving efficiency — especially valuable as data volumes grow exponentially.\n\nLossless compression preserves data exactly, allowing perfect reconstruction. Algorithms: DEFLATE (used in ZIP and gzip), LZ4 (extremely fast, moderate ratio — good for real-time streaming), ZSTD (excellent ratio and speed balance, widely adopted), Brotli (web content). Used for documents, code, executables, databases, and backups.\n\nLossy compression discards data deemed imperceptible to human senses. JPEG (images), MP3/AAC (audio), H.264/H.265/AV1 (video) achieve much higher compression ratios than lossless — but are only appropriate for media, never for data requiring exact reproduction.\n\nDeduplication eliminates redundant data blocks across stored files and backup sets. Inline deduplication processes data as written (higher CPU, immediate savings). Post-process deduplication runs as a background job. Variable-length block deduplication handles shifted data better than fixed-block approaches. Backup systems achieve 10:1 or higher dedup ratios on virtual machine images because most blocks are identical across snapshots.\n\nStorage efficiency technologies — thin provisioning, compression, and deduplication — work best combined. Modern enterprise storage arrays (NetApp ONTAP, Pure Storage, Dell PowerStore) apply all three automatically, transparently maximizing capacity utilization."),
                ("Object Storage and Modern Architectures", "Object storage manages data as discrete objects rather than files in a hierarchy or blocks on a disk, making it the foundation of modern cloud storage at petabyte-to-exabyte scale.\n\nObject storage structure: each object consists of data, metadata (descriptive key-value pairs), and a globally unique identifier. Objects are stored in flat namespaces called buckets — there is no directory hierarchy. Access is via HTTP APIs using standard methods (GET, PUT, DELETE, HEAD, LIST).\n\nKey characteristics: massive scalability (no practical capacity limit), high durability (11 nines via erasure coding across geographic locations), cost-effectiveness for large volumes of unstructured data, and a universal API (the Amazon S3 API is the de facto standard, implemented by Azure Blob, Google Cloud Storage, MinIO, Ceph, and Backblaze B2).\n\nIdeal workloads: write-once, read-many data — images, videos, ML training datasets, log files, backups, and static web assets. Object storage is not suitable for workloads needing low-latency random read/write (databases, running VMs) — use block storage for those.\n\nModern application architectures decouple storage from compute: microservices store artifacts in object storage, serverless functions read and write objects, and data lakes use object storage as a universal, cost-effective data layer. Content Delivery Networks (CDNs) integrate directly with object storage to serve assets globally with low latency.")
            }}
        };

        foreach (var category in categories)
        {
            foreach (var subcategory in category.Subcategories)
            {
                if (!moreLessons.TryGetValue(subcategory.Id, out var extraLessons))
                    continue;

                foreach (var lesson in extraLessons)
                {
                    subcategory.Lessons.Add(new Lesson
                    {
                        Id = lessonId++,
                        SubcategoryId = subcategory.Id,
                        Order = subcategory.Lessons.Count + 1,
                        Title = lesson.Title,
                        Content = lesson.Content
                    });
                }
            }
        }

        return categories;
    }

    private static List<Quiz> BuildQuizzes()
    {
        var quizzes = new List<Quiz>();
        int qId = 1;

        // Lesson 1: What is a Computer?
        quizzes.Add(new Quiz { Id = qId++, LessonId = 1, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the primary component that performs calculations in a computer?", Options = new() { "RAM", "CPU", "SSD", "GPU" }, CorrectIndex = 1, Explanation = "The CPU (Central Processing Unit) is the brain of the computer that executes instructions and performs calculations." },
            new() { Id = qId++, Text = "What does RAM stand for?", Options = new() { "Read Access Memory", "Random Access Memory", "Rapid Access Module", "Read Accessible Memory" }, CorrectIndex = 1, Explanation = "RAM stands for Random Access Memory. It's the short-term memory that temporarily stores data the CPU is actively using." },
            new() { Id = qId++, Text = "Which type of storage keeps data even when the computer is off?", Options = new() { "RAM", "Cache", "HDD/SSD", "CPU registers" }, CorrectIndex = 2, Explanation = "HDD and SSD are persistent (non-volatile) storage devices that retain data without power, unlike RAM which loses data when power is cut." }
        }});

        // Lesson 2: Operating Systems
        quizzes.Add(new Quiz { Id = qId++, LessonId = 2, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the core component of an operating system?", Options = new() { "Shell", "GUI", "Kernel", "Driver" }, CorrectIndex = 2, Explanation = "The kernel is the core of the OS that directly manages hardware resources, including memory, processes, and device I/O." },
            new() { Id = qId++, Text = "Which of the following is NOT a popular operating system?", Options = new() { "Windows", "macOS", "Linux", "Oracle" }, CorrectIndex = 3, Explanation = "Oracle is a database company, not an operating system. Windows, macOS, and Linux are all widely-used operating systems." },
            new() { Id = qId++, Text = "What is virtual memory?", Options = new() { "A type of RAM", "Using disk space as extra RAM", "A virtual machine", "Cloud storage" }, CorrectIndex = 1, Explanation = "Virtual memory is a technique where the OS uses a portion of the hard drive as if it were RAM, extending available memory at the cost of speed." }
        }});

        // Lesson 3: Intro to Cybersecurity
        quizzes.Add(new Quiz { Id = qId++, LessonId = 3, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does the 'C' in the CIA Triad stand for?", Options = new() { "Control", "Confidentiality", "Compliance", "Cryptography" }, CorrectIndex = 1, Explanation = "The CIA Triad stands for Confidentiality, Integrity, and Availability — the three core pillars of information security." },
            new() { Id = qId++, Text = "What type of attack tricks users into revealing their credentials?", Options = new() { "DDoS", "Ransomware", "Phishing", "SQL Injection" }, CorrectIndex = 2, Explanation = "Phishing is a social engineering attack where attackers impersonate trusted entities to steal credentials or sensitive information." },
            new() { Id = qId++, Text = "Which tool monitors network traffic for suspicious activity?", Options = new() { "Firewall", "Antivirus", "IDS (Intrusion Detection System)", "VPN" }, CorrectIndex = 2, Explanation = "An Intrusion Detection System (IDS) monitors network traffic for suspicious patterns and alerts administrators to potential threats." }
        }});

        // Lesson 4: Encryption and Passwords
        quizzes.Add(new Quiz { Id = qId++, LessonId = 4, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which encryption type uses the same key for encryption and decryption?", Options = new() { "Asymmetric", "Symmetric", "Hashing", "Public-key" }, CorrectIndex = 1, Explanation = "Symmetric encryption uses a single shared key for both encryption and decryption. AES is a common symmetric algorithm." },
            new() { Id = qId++, Text = "What is the minimum recommended password length for strong security?", Options = new() { "6 characters", "8 characters", "12 characters", "20 characters" }, CorrectIndex = 2, Explanation = "Security experts recommend at least 12 characters for a strong password, combining uppercase, lowercase, numbers, and symbols." },
            new() { Id = qId++, Text = "What does 2FA add to the login process?", Options = new() { "A second password", "A second verification step", "Biometric scan only", "IP address check" }, CorrectIndex = 1, Explanation = "Two-factor authentication (2FA) adds a second verification step beyond the password, such as a one-time code sent to your phone." }
        }});

        // Lesson 5: Python Variables
        quizzes.Add(new Quiz { Id = qId++, LessonId = 5, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the data type of the value True in Python?", Options = new() { "int", "str", "bool", "float" }, CorrectIndex = 2, Explanation = "True and False are boolean (bool) values in Python. They are a subclass of int, where True == 1 and False == 0." },
            new() { Id = qId++, Text = "Which Python data structure is ordered and immutable?", Options = new() { "List", "Dictionary", "Set", "Tuple" }, CorrectIndex = 3, Explanation = "A tuple is an ordered, immutable sequence. Once created, its elements cannot be changed, unlike a list." },
            new() { Id = qId++, Text = "How do you check the type of a variable in Python?", Options = new() { "typeof(x)", "type(x)", "x.type()", "gettype(x)" }, CorrectIndex = 1, Explanation = "The built-in type() function returns the type of any object in Python, e.g., type(42) returns <class 'int'>." }
        }});

        // Lesson 6: Control Flow
        quizzes.Add(new Quiz { Id = qId++, LessonId = 6, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What keyword is used to exit a loop early in Python?", Options = new() { "exit", "return", "break", "stop" }, CorrectIndex = 2, Explanation = "The break keyword immediately exits the enclosing loop and continues execution after it." },
            new() { Id = qId++, Text = "What does the continue statement do in a loop?", Options = new() { "Exits the loop", "Skips to the next iteration", "Pauses execution", "Returns a value" }, CorrectIndex = 1, Explanation = "continue skips the rest of the current iteration and moves to the next one, without exiting the loop." },
            new() { Id = qId++, Text = "What is the output of: [x*2 for x in range(3)]?", Options = new() { "[0, 2, 4]", "[2, 4, 6]", "[0, 1, 2]", "[1, 2, 3]" }, CorrectIndex = 0, Explanation = "range(3) produces 0, 1, 2. Multiplying each by 2 gives [0, 2, 4]. This is a list comprehension." }
        }});

        // Lesson 7: HTML Basics
        quizzes.Add(new Quiz { Id = qId++, LessonId = 7, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does HTML stand for?", Options = new() { "Hyper Text Markup Language", "High Tech Modern Language", "Hyperlink Text Machine Language", "Hyper Transfer Markup Language" }, CorrectIndex = 0, Explanation = "HTML stands for HyperText Markup Language. It is the standard language for structuring web page content." },
            new() { Id = qId++, Text = "Which tag is used to create a hyperlink?", Options = new() { "<link>", "<href>", "<a>", "<url>" }, CorrectIndex = 2, Explanation = "The <a> (anchor) tag creates hyperlinks. The destination is specified with the href attribute: <a href='url'>text</a>." },
            new() { Id = qId++, Text = "Which is an example of a semantic HTML element?", Options = new() { "<div>", "<span>", "<article>", "<b>" }, CorrectIndex = 2, Explanation = "<article> is a semantic element that describes its content's meaning. <div> and <span> are non-semantic layout containers." }
        }});

        // Lesson 8: CSS Styling
        quizzes.Add(new Quiz { Id = qId++, LessonId = 8, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "In CSS, what does the box model include from inside to outside?", Options = new() { "Content, Border, Padding, Margin", "Content, Padding, Border, Margin", "Margin, Border, Padding, Content", "Padding, Content, Border, Margin" }, CorrectIndex = 1, Explanation = "The CSS box model from inside out is: Content → Padding → Border → Margin." },
            new() { Id = qId++, Text = "Which CSS property makes elements display in a flexible row or column?", Options = new() { "display: block", "display: flex", "display: inline", "display: grid" }, CorrectIndex = 1, Explanation = "display: flex enables the Flexbox layout model, arranging children in a row (default) or column." },
            new() { Id = qId++, Text = "What CSS feature enables responsive design based on screen size?", Options = new() { "Animation", "Media queries", "Pseudo-classes", "Variables" }, CorrectIndex = 1, Explanation = "Media queries (@media) allow different styles to be applied based on device characteristics like screen width, enabling responsive design." }
        }});

        // Lesson 9: English Tenses
        quizzes.Add(new Quiz { Id = qId++, LessonId = 9, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which tense is used to describe an action in progress right now?", Options = new() { "Simple Present", "Present Continuous", "Present Perfect", "Simple Future" }, CorrectIndex = 1, Explanation = "Present Continuous (am/is/are + -ing) describes an action happening at the moment of speaking." },
            new() { Id = qId++, Text = "Which sentence is in Present Perfect tense?", Options = new() { "She walks to school.", "She walked to school.", "She has walked to school.", "She will walk to school." }, CorrectIndex = 2, Explanation = "Present Perfect uses 'have/has + past participle'. It connects a past action to the present moment." },
            new() { Id = qId++, Text = "How many main tenses does English have?", Options = new() { "3", "6", "12", "16" }, CorrectIndex = 2, Explanation = "English has 12 main tenses: simple, continuous, perfect, and perfect continuous forms for past, present, and future." }
        }});

        // Lesson 10: Parts of Speech
        quizzes.Add(new Quiz { Id = qId++, LessonId = 10, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What part of speech modifies a verb, adjective, or other adverb?", Options = new() { "Adjective", "Noun", "Adverb", "Conjunction" }, CorrectIndex = 2, Explanation = "Adverbs modify verbs (run quickly), adjectives (very tall), or other adverbs (quite slowly)." },
            new() { Id = qId++, Text = "What do conjunctions do in a sentence?", Options = new() { "Name things", "Show emotion", "Connect words or clauses", "Replace nouns" }, CorrectIndex = 2, Explanation = "Conjunctions (and, but, or, because, although) connect words, phrases, or clauses in a sentence." },
            new() { Id = qId++, Text = "Which word is a pronoun?", Options = new() { "Run", "Beautiful", "They", "Quickly" }, CorrectIndex = 2, Explanation = "'They' is a pronoun — it replaces a noun (a group of people). 'Run' is a verb, 'Beautiful' is an adjective, 'Quickly' is an adverb." }
        }});

        // Lesson 11: Spanish Greetings
        quizzes.Add(new Quiz { Id = qId++, LessonId = 11, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "How do you say 'Good morning' in Spanish?", Options = new() { "Buenas noches", "Buenas tardes", "Buenos días", "Hola" }, CorrectIndex = 2, Explanation = "Buenos días means Good morning. Buenas tardes = Good afternoon, Buenas noches = Good evening/night." },
            new() { Id = qId++, Text = "What does 'Mucho gusto' mean?", Options = new() { "Thank you very much", "Nice to meet you", "Good morning", "How are you?" }, CorrectIndex = 1, Explanation = "'Mucho gusto' literally means 'much pleasure' and is used to say 'Nice to meet you' when being introduced." },
            new() { Id = qId++, Text = "Which form of 'you' is used formally in Spanish?", Options = new() { "tú", "usted", "vos", "nosotros" }, CorrectIndex = 1, Explanation = "'Usted' is the formal form of 'you' in Spanish, used with strangers, elders, or in professional contexts. 'Tú' is informal." }
        }});

        // Lesson 12: Spanish Numbers and Colors
        quizzes.Add(new Quiz { Id = qId++, LessonId = 12, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is 'five' in Spanish?", Options = new() { "cuatro", "seis", "cinco", "siete" }, CorrectIndex = 2, Explanation = "cinco = five. cuatro = four, seis = six, siete = seven." },
            new() { Id = qId++, Text = "How do you say 'blue' in Spanish?", Options = new() { "verde", "rojo", "azul", "amarillo" }, CorrectIndex = 2, Explanation = "azul = blue. verde = green, rojo = red, amarillo = yellow." },
            new() { Id = qId++, Text = "In Spanish, colors must agree with the noun in:", Options = new() { "Length only", "Gender and number", "Pronunciation only", "Spelling only" }, CorrectIndex = 1, Explanation = "Spanish adjectives, including colors, must match the noun they describe in gender (masculine/feminine) and number (singular/plural)." }
        }});

        // Lesson 13: What is a Network?
        quizzes.Add(new Quiz { Id = qId++, LessonId = 13, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What type of network covers a small area like a home or office?", Options = new() { "WAN", "MAN", "LAN", "PAN" }, CorrectIndex = 2, Explanation = "LAN (Local Area Network) covers a small area like a home, office, or building." },
            new() { Id = qId++, Text = "Which device connects different networks together?", Options = new() { "Switch", "Hub", "Router", "Modem" }, CorrectIndex = 2, Explanation = "A router connects different networks (e.g., your home network to the internet) and routes traffic between them." },
            new() { Id = qId++, Text = "In a client-server model, what does the server do?", Options = new() { "Requests services", "Provides services", "Routes traffic", "Stores addresses" }, CorrectIndex = 1, Explanation = "In a client-server model, servers provide services (file storage, web pages, databases) while clients request those services." }
        }});

        // Lesson 14: IP Addressing
        quizzes.Add(new Quiz { Id = qId++, LessonId = 14, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "How many bits does an IPv4 address have?", Options = new() { "16", "32", "64", "128" }, CorrectIndex = 1, Explanation = "IPv4 addresses are 32 bits long, written as four 8-bit octets (e.g., 192.168.1.1), providing about 4.3 billion unique addresses." },
            new() { Id = qId++, Text = "Which IP range is private and not routable on the internet?", Options = new() { "8.8.8.0/24", "172.16.0.0/12", "1.1.1.0/24", "216.58.0.0/16" }, CorrectIndex = 1, Explanation = "172.16.0.0/12 is a private IP range. Private ranges (10.x, 172.16-31.x, 192.168.x) are not routed on the public internet." },
            new() { Id = qId++, Text = "What does NAT stand for?", Options = new() { "Network Address Translation", "Node Address Table", "Network Access Terminal", "Null Address Token" }, CorrectIndex = 0, Explanation = "NAT (Network Address Translation) allows multiple devices on a private network to share a single public IP address." }
        }});

        // Lesson 15: OSI Model
        quizzes.Add(new Quiz { Id = qId++, LessonId = 15, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "How many layers does the OSI model have?", Options = new() { "4", "5", "6", "7" }, CorrectIndex = 3, Explanation = "The OSI model has 7 layers: Physical, Data Link, Network, Transport, Session, Presentation, and Application." },
            new() { Id = qId++, Text = "At which OSI layer does IP addressing occur?", Options = new() { "Layer 2 (Data Link)", "Layer 3 (Network)", "Layer 4 (Transport)", "Layer 7 (Application)" }, CorrectIndex = 1, Explanation = "Layer 3 (Network) handles logical addressing (IP addresses) and routing of packets across networks." },
            new() { Id = qId++, Text = "Which protocol operates at the Application layer (Layer 7)?", Options = new() { "TCP", "IP", "Ethernet", "HTTP" }, CorrectIndex = 3, Explanation = "HTTP is an Application layer protocol. TCP is Transport layer, IP is Network layer, Ethernet is Data Link/Physical." }
        }});

        // Lesson 16: TCP vs UDP
        quizzes.Add(new Quiz { Id = qId++, LessonId = 16, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What type of protocol is TCP?", Options = new() { "Connectionless", "Connection-oriented", "Broadcast", "Anycast" }, CorrectIndex = 1, Explanation = "TCP is connection-oriented — it establishes a connection via a three-way handshake before data transfer." },
            new() { Id = qId++, Text = "Which protocol would be best for video streaming?", Options = new() { "TCP", "SMTP", "UDP", "FTP" }, CorrectIndex = 2, Explanation = "UDP is preferred for video streaming because its low overhead and speed are more important than guaranteed delivery — a few lost packets are acceptable." },
            new() { Id = qId++, Text = "What is the standard port for HTTPS?", Options = new() { "80", "22", "443", "8080" }, CorrectIndex = 2, Explanation = "HTTPS uses port 443. HTTP uses port 80, SSH uses port 22." }
        }});

        // Lesson 17: What is ML?
        quizzes.Add(new Quiz { Id = qId++, LessonId = 17, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which type of ML uses labeled training data?", Options = new() { "Unsupervised learning", "Reinforcement learning", "Supervised learning", "Self-supervised learning" }, CorrectIndex = 2, Explanation = "Supervised learning trains on labeled examples (input/output pairs), learning to predict outputs for new inputs." },
            new() { Id = qId++, Text = "What is the typical order of steps in an ML workflow?", Options = new() { "Train → Collect data → Evaluate → Deploy", "Collect data → Train → Evaluate → Deploy", "Deploy → Collect data → Train → Evaluate", "Evaluate → Train → Collect data → Deploy" }, CorrectIndex = 1, Explanation = "The typical ML workflow: collect data → prepare data → choose model → train → evaluate → tune → deploy." },
            new() { Id = qId++, Text = "Which Python library is commonly used for machine learning?", Options = new() { "NumPy", "pandas", "scikit-learn", "Flask" }, CorrectIndex = 2, Explanation = "scikit-learn is a popular Python library providing simple and efficient tools for machine learning tasks." }
        }});

        // Lesson 18: Supervised Learning
        quizzes.Add(new Quiz { Id = qId++, LessonId = 18, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the goal of a classification task?", Options = new() { "Predict a continuous value", "Predict a category", "Cluster data points", "Reduce dimensions" }, CorrectIndex = 1, Explanation = "Classification predicts which category an input belongs to (e.g., spam/not spam, cat/dog)." },
            new() { Id = qId++, Text = "What happens when a model overfits?", Options = new() { "It performs well on new data", "It is too simple to learn patterns", "It memorizes training data but fails on new data", "It trains too slowly" }, CorrectIndex = 2, Explanation = "Overfitting means the model has learned the training data too well, including noise, and doesn't generalize to new unseen data." },
            new() { Id = qId++, Text = "What technique is used to assess a model's generalizability?", Options = new() { "Gradient descent", "Cross-validation", "Backpropagation", "Regularization" }, CorrectIndex = 1, Explanation = "Cross-validation (e.g., k-fold) evaluates a model by training and testing on different subsets of data to estimate real-world performance." }
        }});

        // Lesson 19: How Neural Networks Work
        quizzes.Add(new Quiz { Id = qId++, LessonId = 19, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the role of the activation function in a neural network?", Options = new() { "Initialize weights", "Introduce non-linearity", "Split data into batches", "Measure accuracy" }, CorrectIndex = 1, Explanation = "Activation functions (like ReLU, Sigmoid) introduce non-linearity, enabling networks to learn complex patterns beyond linear relationships." },
            new() { Id = qId++, Text = "What algorithm is used to update weights during training?", Options = new() { "Forward propagation", "Backpropagation", "Cross-validation", "Regularization" }, CorrectIndex = 1, Explanation = "Backpropagation calculates gradients of the loss function with respect to each weight, enabling gradient descent to update weights." },
            new() { Id = qId++, Text = "What is one full pass through the entire training dataset called?", Options = new() { "Batch", "Step", "Epoch", "Iteration" }, CorrectIndex = 2, Explanation = "An epoch is one complete pass through the entire training dataset. Models are typically trained for multiple epochs." }
        }});

        // Lesson 20: Deep Learning Applications
        quizzes.Add(new Quiz { Id = qId++, LessonId = 20, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which neural network architecture is specialized for image processing?", Options = new() { "RNN", "LSTM", "CNN", "Transformer" }, CorrectIndex = 2, Explanation = "CNNs (Convolutional Neural Networks) use convolution operations to detect spatial features in images like edges, shapes, and textures." },
            new() { Id = qId++, Text = "What architecture powers modern Large Language Models?", Options = new() { "CNN", "RNN", "LSTM", "Transformer" }, CorrectIndex = 3, Explanation = "Transformers use self-attention mechanisms and are the architecture behind GPT, BERT, and other powerful language models." },
            new() { Id = qId++, Text = "What does GAN stand for?", Options = new() { "Gradient Adjustment Node", "Generative Adversarial Network", "General Attention Network", "Graph Attention Node" }, CorrectIndex = 1, Explanation = "GAN stands for Generative Adversarial Network — two networks (generator and discriminator) compete to produce realistic synthetic data." }
        }});

        // Lesson 21: Effective Communication
        quizzes.Add(new Quiz { Id = qId++, LessonId = 21, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is active listening?", Options = new() { "Hearing background noise", "Thinking about your response while others speak", "Fully concentrating on and understanding the speaker", "Listening to audio recordings" }, CorrectIndex = 2, Explanation = "Active listening means fully concentrating on the speaker to understand their message, not just hearing words." },
            new() { Id = qId++, Text = "What does non-verbal communication include?", Options = new() { "Written emails", "Phone calls", "Body language and facial expressions", "Instant messages" }, CorrectIndex = 2, Explanation = "Non-verbal communication includes body language, facial expressions, gestures, posture, eye contact, and tone of voice." },
            new() { Id = qId++, Text = "What is a communication barrier?", Options = new() { "A good listening technique", "Anything that distorts or blocks a message", "A type of presentation style", "A written communication tool" }, CorrectIndex = 1, Explanation = "Communication barriers are obstacles that prevent messages from being clearly transmitted or understood, such as noise, language differences, or emotional state." }
        }});

        // Lesson 22: Public Speaking
        quizzes.Add(new Quiz { Id = qId++, LessonId = 22, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which part of a speech is most memorable?", Options = new() { "The middle", "Opening and closing", "Only the opening", "The statistical data" }, CorrectIndex = 1, Explanation = "The opening and closing of a speech are most remembered. A strong hook and memorable ending leave the greatest impression." },
            new() { Id = qId++, Text = "What is the best way to manage public speaking nerves?", Options = new() { "Avoid preparation to stay spontaneous", "Read directly from notes", "Practice and reframe anxiety as excitement", "Speak as quickly as possible" }, CorrectIndex = 2, Explanation = "Consistent practice builds confidence. Reframing nervousness as excitement (same physiological response) helps performance." },
            new() { Id = qId++, Text = "What is a good way to start a speech?", Options = new() { "An apology for your lack of preparation", "A long list of agenda items", "A hook like a story or surprising fact", "Thanking everyone immediately" }, CorrectIndex = 2, Explanation = "Starting with a hook (story, question, surprising fact, quote) captures the audience's attention immediately." }
        }});

        // Lesson 23: Leadership Styles
        quizzes.Add(new Quiz { Id = qId++, LessonId = 23, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which leadership style involves the team in decision-making?", Options = new() { "Autocratic", "Laissez-faire", "Democratic", "Transactional" }, CorrectIndex = 2, Explanation = "Democratic (participative) leadership involves team members in decisions, increasing buy-in, creativity, and morale." },
            new() { Id = qId++, Text = "Which style is most effective in a crisis requiring quick decisions?", Options = new() { "Servant leadership", "Democratic leadership", "Transformational leadership", "Autocratic leadership" }, CorrectIndex = 3, Explanation = "Autocratic leadership allows fast, decisive action without consultation — useful in crises but can damage morale long-term." },
            new() { Id = qId++, Text = "What does Situational Leadership theory suggest?", Options = new() { "Use only one leadership style", "Adapt style based on team member's competence and commitment", "Always be democratic", "Leadership cannot be learned" }, CorrectIndex = 1, Explanation = "Situational leadership says effective leaders adjust their style based on each follower's development level (competence + commitment)." }
        }});

        // Lesson 24: Emotional Intelligence
        quizzes.Add(new Quiz { Id = qId++, LessonId = 24, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What are the 5 components of Goleman's Emotional Intelligence model?", Options = new() { "IQ, EQ, AQ, CQ, SQ", "Self-awareness, Self-regulation, Motivation, Empathy, Social skills", "Leadership, Communication, Teamwork, Creativity, Adaptability", "Thinking, Feeling, Sensing, Intuiting, Judging" }, CorrectIndex = 1, Explanation = "Goleman's EI model: Self-awareness, Self-regulation, Motivation, Empathy, and Social skills." },
            new() { Id = qId++, Text = "What is empathy in the context of emotional intelligence?", Options = new() { "Feeling sorry for others", "Understanding and sharing others' feelings", "Suppressing your own emotions", "Being overly agreeable" }, CorrectIndex = 1, Explanation = "Empathy is the ability to understand and share the feelings of another person — seeing situations from their perspective." },
            new() { Id = qId++, Text = "How can you develop self-awareness?", Options = new() { "Avoid feedback from others", "Practice mindfulness and journaling", "Focus only on others' emotions", "Suppress negative feelings" }, CorrectIndex = 1, Explanation = "Mindfulness, journaling, and seeking feedback help develop self-awareness by increasing insight into your own emotions, thoughts, and behaviors." }
        }});

        // Lesson 25: CPU and Motherboard
        quizzes.Add(new Quiz { Id = qId++, LessonId = 25, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does CPU stand for?", Options = new() { "Central Power Unit", "Central Processing Unit", "Computer Processing Unit", "Core Processing Utility" }, CorrectIndex = 1, Explanation = "CPU stands for Central Processing Unit — the primary component that executes instructions and performs calculations." },
            new() { Id = qId++, Text = "What is the purpose of cache memory in a CPU?", Options = new() { "Long-term data storage", "Ultra-fast temporary storage near the processor", "Connecting to the internet", "Running the operating system" }, CorrectIndex = 1, Explanation = "CPU cache (L1, L2, L3) is ultra-fast memory built into or near the processor to reduce latency when accessing frequently used data." },
            new() { Id = qId++, Text = "What does UEFI replace in modern computers?", Options = new() { "RAM", "BIOS", "CPU", "GPU" }, CorrectIndex = 1, Explanation = "UEFI (Unified Extensible Firmware Interface) is the modern replacement for the older BIOS firmware, offering faster boot times and more features." }
        }});

        // Lesson 26: RAM and GPU
        quizzes.Add(new Quiz { Id = qId++, LessonId = 26, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What happens to data in RAM when you turn off the computer?", Options = new() { "It is saved automatically", "It is transferred to the SSD", "It is lost", "It stays in cache" }, CorrectIndex = 2, Explanation = "RAM is volatile memory — all data is lost when power is removed. This is why you need to save files to persistent storage." },
            new() { Id = qId++, Text = "What is VRAM?", Options = new() { "Virtual RAM", "Video RAM used by the GPU", "Variable Rate Memory", "A type of SSD" }, CorrectIndex = 1, Explanation = "VRAM (Video RAM) is memory on a dedicated GPU card, used to store textures, frame buffers, and other graphics data." },
            new() { Id = qId++, Text = "What is thermal throttling?", Options = new() { "Overclocking a CPU", "When a processor reduces speed to prevent overheating", "A type of cooling system", "Undervolting the GPU" }, CorrectIndex = 1, Explanation = "Thermal throttling occurs when a CPU or GPU automatically reduces its clock speed to lower heat output and prevent damage from overheating." }
        }});

        // Lesson 27: HDD vs SSD
        quizzes.Add(new Quiz { Id = qId++, LessonId = 27, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "How does an HDD store data?", Options = new() { "In flash memory chips", "On spinning magnetic platters", "In optical discs", "In volatile RAM chips" }, CorrectIndex = 1, Explanation = "HDDs store data on spinning magnetic platters that are read by a mechanical arm, making them slower but cheap for large storage." },
            new() { Id = qId++, Text = "Which SSD type offers the fastest speeds?", Options = new() { "SATA SSD", "USB SSD", "NVMe M.2 SSD", "External SSD" }, CorrectIndex = 2, Explanation = "NVMe M.2 SSDs use the PCIe bus and achieve 3,000–7,000 MB/s, far faster than SATA SSDs (~550 MB/s)." },
            new() { Id = qId++, Text = "What is the purpose of TRIM in SSDs?", Options = new() { "Increases write speed temporarily", "Maintains performance by managing deleted data", "Encrypts stored data", "Backs up data automatically" }, CorrectIndex = 1, Explanation = "TRIM tells the SSD which blocks of data are no longer in use, allowing the drive to clean them up proactively and maintain write performance." }
        }});

        // Lesson 28: RAID and Backup
        quizzes.Add(new Quiz { Id = qId++, LessonId = 28, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which RAID level provides no redundancy but maximum performance?", Options = new() { "RAID 1", "RAID 5", "RAID 0", "RAID 10" }, CorrectIndex = 2, Explanation = "RAID 0 (striping) splits data across drives for maximum speed but offers zero redundancy — one drive failure means total data loss." },
            new() { Id = qId++, Text = "What does the 3-2-1 backup rule recommend?", Options = new() { "3 backups on 1 device updated 2 times daily", "3 copies, 2 media types, 1 offsite copy", "3 servers, 2 locations, 1 cloud provider", "Backup 3 times daily to 2 locations using 1 tool" }, CorrectIndex = 1, Explanation = "The 3-2-1 rule: keep 3 copies of data on 2 different media types with 1 copy stored offsite for disaster recovery." },
            new() { Id = qId++, Text = "Is RAID a substitute for backups?", Options = new() { "Yes, RAID 1 protects against all data loss", "Yes, any RAID level is sufficient", "No, RAID protects against hardware failure but not deletion or ransomware", "It depends on the RAID level" }, CorrectIndex = 2, Explanation = "RAID is NOT a backup. It protects against drive failure but not accidental deletion, ransomware, fire, or theft. Dedicated backups are still essential." }
        }});

        // Lesson 29: Computer Hardware Components
        quizzes.Add(new Quiz { Id = qId++, LessonId = 29, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which component primarily executes instructions in a computer?", Options = new() { "RAM", "CPU", "SSD", "Motherboard battery" }, CorrectIndex = 1, Explanation = "The CPU executes program instructions and performs calculations." },
            new() { Id = qId++, Text = "What is the main role of RAM?", Options = new() { "Permanent storage", "Network routing", "Temporary active memory", "Power conversion" }, CorrectIndex = 2, Explanation = "RAM stores active data for fast access while programs are running." },
            new() { Id = qId++, Text = "What is a common bottleneck in many older systems?", Options = new() { "Too many keyboard keys", "Slow storage drives", "Too many USB ports", "High monitor refresh rate" }, CorrectIndex = 1, Explanation = "Slow storage can bottleneck overall responsiveness even with a decent CPU." }
        }});

        // Lesson 30: Network Security Fundamentals
        quizzes.Add(new Quiz { Id = qId++, LessonId = 30, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is a key principle of zero trust security?", Options = new() { "Trust all internal users", "Trust all VPN users", "Never trust by default", "Disable all monitoring" }, CorrectIndex = 2, Explanation = "Zero trust assumes no user or device is trusted by default." },
            new() { Id = qId++, Text = "Which control limits attacker movement inside a network?", Options = new() { "Network segmentation", "Longer monitor cables", "Higher Wi-Fi channels", "External SSD backups only" }, CorrectIndex = 0, Explanation = "Segmentation limits lateral movement by isolating network zones." },
            new() { Id = qId++, Text = "Why are network logs important?", Options = new() { "They replace backups", "They help detect suspicious behavior", "They encrypt traffic", "They update drivers automatically" }, CorrectIndex = 1, Explanation = "Logs help teams detect and investigate suspicious network activity." }
        }});

        // Lesson 31: Functions and Modules
        quizzes.Add(new Quiz { Id = qId++, LessonId = 31, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "How do you define a function in Python?", Options = new() { "func myFunction()", "def my_function():", "function myFunction()", "define my_function()" }, CorrectIndex = 1, Explanation = "Python uses the def keyword to define functions." },
            new() { Id = qId++, Text = "What is a Python module?", Options = new() { "A browser plugin", "A Python file with reusable code", "A CPU core", "A type of variable" }, CorrectIndex = 1, Explanation = "A module is a Python file containing code you can import and reuse." },
            new() { Id = qId++, Text = "Why split code into modules?", Options = new() { "To make files longer", "To avoid function parameters", "To improve organization and reuse", "To disable imports" }, CorrectIndex = 2, Explanation = "Modular code is easier to maintain, test, and reuse." }
        }});

        // Lesson 32: JavaScript Fundamentals
        quizzes.Add(new Quiz { Id = qId++, LessonId = 32, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does the DOM represent in web development?", Options = new() { "Database Object Model", "Document Object Model", "Dynamic Output Method", "Distributed Object Map" }, CorrectIndex = 1, Explanation = "DOM stands for Document Object Model, representing page structure in code." },
            new() { Id = qId++, Text = "Which syntax handles asynchronous code in modern JavaScript?", Options = new() { "switch/case only", "goto blocks", "async/await", "typedef" }, CorrectIndex = 2, Explanation = "async/await simplifies working with asynchronous operations." },
            new() { Id = qId++, Text = "What is a common use of JavaScript events?", Options = new() { "Designing CPU chips", "Responding to user actions", "Compiling C# code", "Formatting hard drives" }, CorrectIndex = 1, Explanation = "Events let JavaScript respond to actions like clicks and input changes." }
        }});

        // Lesson 33: Sentence Structure
        quizzes.Add(new Quiz { Id = qId++, LessonId = 33, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is typical English sentence order?", Options = new() { "Verb + Subject + Object", "Subject + Verb + Object", "Object + Verb + Subject", "Subject + Object + Verb" }, CorrectIndex = 1, Explanation = "Standard English order is Subject + Verb + Object." },
            new() { Id = qId++, Text = "A sentence with two independent clauses is usually:", Options = new() { "Simple", "Fragmented", "Compound", "Imperative only" }, CorrectIndex = 2, Explanation = "Compound sentences connect two independent clauses." },
            new() { Id = qId++, Text = "Why vary sentence length in writing?", Options = new() { "To confuse readers", "To improve rhythm and clarity", "To remove punctuation", "To avoid transitions" }, CorrectIndex = 1, Explanation = "Sentence variety improves flow and readability." }
        }});

        // Lesson 34: Common Spanish Verbs
        quizzes.Add(new Quiz { Id = qId++, LessonId = 34, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which verb pair both means 'to be' in Spanish?", Options = new() { "tener and hacer", "ir and venir", "ser and estar", "poder and querer" }, CorrectIndex = 2, Explanation = "Ser and estar both translate as 'to be' but are used differently." },
            new() { Id = qId++, Text = "Which verb is commonly used for possession (to have)?", Options = new() { "tener", "ser", "ir", "estar" }, CorrectIndex = 0, Explanation = "Tener means 'to have' and is frequently used in daily conversation." },
            new() { Id = qId++, Text = "What improves verb fluency most?", Options = new() { "Only reading grammar charts", "Avoiding speaking", "Regular speaking practice with common verbs", "Memorizing random nouns only" }, CorrectIndex = 2, Explanation = "Frequent spoken practice with common verbs accelerates fluency." }
        }});

        // Lesson 35: Network Topologies
        quizzes.Add(new Quiz { Id = qId++, LessonId = 35, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which topology is common in modern LANs and easy to manage?", Options = new() { "Bus", "Ring", "Star", "Fully disconnected" }, CorrectIndex = 2, Explanation = "Star topology is common because failures are easier to isolate." },
            new() { Id = qId++, Text = "Which topology provides strong redundancy with multiple paths?", Options = new() { "Mesh", "Bus", "Point-to-point only", "Single-node" }, CorrectIndex = 0, Explanation = "Mesh topology provides multiple paths and high fault tolerance." },
            new() { Id = qId++, Text = "Logical topology describes:", Options = new() { "Cable color", "Data flow patterns", "Rack weight", "Power adapter type" }, CorrectIndex = 1, Explanation = "Logical topology explains how data moves, not just physical cabling." }
        }});

        // Lesson 36: Application Layer Protocols
        quizzes.Add(new Quiz { Id = qId++, LessonId = 36, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which protocol translates domain names to IP addresses?", Options = new() { "SMTP", "DNS", "FTP", "SSH" }, CorrectIndex = 1, Explanation = "DNS resolves domain names to IP addresses." },
            new() { Id = qId++, Text = "What does HTTPS add compared to HTTP?", Options = new() { "More ads", "Transport encryption via TLS", "Smaller IP addresses", "No status codes" }, CorrectIndex = 1, Explanation = "HTTPS adds TLS encryption for confidentiality and integrity." },
            new() { Id = qId++, Text = "Which protocol is primarily used to send email?", Options = new() { "IMAP", "SMTP", "POP3", "SNMP" }, CorrectIndex = 1, Explanation = "SMTP is the core protocol for sending email." }
        }});

        // Lesson 37: Model Evaluation Metrics
        quizzes.Add(new Quiz { Id = qId++, LessonId = 37, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which metric is especially useful when false negatives matter?", Options = new() { "Recall", "Only accuracy", "Clock speed", "Latency percent" }, CorrectIndex = 0, Explanation = "Recall measures how many true positives were captured." },
            new() { Id = qId++, Text = "For regression tasks, which metric measures average absolute error?", Options = new() { "F1-score", "MAE", "ROC-AUC", "Precision" }, CorrectIndex = 1, Explanation = "MAE is Mean Absolute Error, common for regression evaluation." },
            new() { Id = qId++, Text = "Why can accuracy be misleading on imbalanced data?", Options = new() { "It ignores all predictions", "It can hide poor minority-class performance", "It only works with text", "It is always zero" }, CorrectIndex = 1, Explanation = "High accuracy may still miss minority classes in imbalanced datasets." }
        }});

        // Lesson 38: Training Neural Networks Effectively
        quizzes.Add(new Quiz { Id = qId++, LessonId = 38, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What hyperparameter strongly affects optimization stability?", Options = new() { "Font size", "Learning rate", "Cable length", "Screen brightness" }, CorrectIndex = 1, Explanation = "Learning rate strongly influences convergence behavior." },
            new() { Id = qId++, Text = "What technique can reduce overfitting in neural networks?", Options = new() { "Dropout", "Ignoring validation data", "Removing all activations", "Always using one epoch" }, CorrectIndex = 0, Explanation = "Dropout is a regularization technique that helps reduce overfitting." },
            new() { Id = qId++, Text = "Why use early stopping?", Options = new() { "To increase overfitting", "To stop training when validation no longer improves", "To skip data preparation", "To disable GPUs" }, CorrectIndex = 1, Explanation = "Early stopping prevents overtraining once validation performance plateaus." }
        }});

        // Lesson 39: Written Communication at Work
        quizzes.Add(new Quiz { Id = qId++, LessonId = 39, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What should most professional messages include?", Options = new() { "Only greetings", "Context, purpose, and requested action", "Unrelated details", "No deadlines ever" }, CorrectIndex = 1, Explanation = "Clear communication includes context, purpose, and expected next steps." },
            new() { Id = qId++, Text = "Why use headings and bullet points?", Options = new() { "To reduce readability", "To improve scanning and clarity", "To remove structure", "To avoid decisions" }, CorrectIndex = 1, Explanation = "Structured formatting makes information easier to scan and understand." },
            new() { Id = qId++, Text = "What is a key risk of ambiguous writing?", Options = new() { "Faster execution", "Fewer misunderstandings", "Misaligned expectations", "Automatic approvals" }, CorrectIndex = 2, Explanation = "Ambiguous writing can cause misunderstandings and rework." }
        }});

        // Lesson 40: Decision-Making for Leaders
        quizzes.Add(new Quiz { Id = qId++, LessonId = 40, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What helps evaluate options objectively?", Options = new() { "Ignoring data", "Impact-vs-effort frameworks", "Choosing randomly", "Delaying forever" }, CorrectIndex = 1, Explanation = "Structured frameworks help leaders compare options consistently." },
            new() { Id = qId++, Text = "After deciding, leaders should communicate:", Options = new() { "Nothing", "Rationale, ownership, and success criteria", "Only budget numbers", "Only technical jargon" }, CorrectIndex = 1, Explanation = "Clear communication enables aligned execution and accountability." },
            new() { Id = qId++, Text = "Why review outcomes after a decision?", Options = new() { "To avoid learning", "To capture lessons and adjust strategy", "To remove accountability", "To delay delivery" }, CorrectIndex = 1, Explanation = "Post-decision review helps improve future decision quality." }
        }});

        // Lesson 41: Power Supply and Cooling
        quizzes.Add(new Quiz { Id = qId++, LessonId = 41, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the PSU's main job in a PC?", Options = new() { "Render graphics", "Convert AC power to stable DC outputs", "Store files", "Manage Wi-Fi" }, CorrectIndex = 1, Explanation = "The PSU converts wall AC into DC voltages required by components." },
            new() { Id = qId++, Text = "What can happen when cooling is insufficient?", Options = new() { "Thermal throttling", "Faster backups", "More CPU cores", "Automatic RAID" }, CorrectIndex = 0, Explanation = "High temperatures can trigger thermal throttling and reduce performance." },
            new() { Id = qId++, Text = "What does an 80 PLUS rating indicate?", Options = new() { "Display quality", "Power efficiency class", "Storage capacity", "Network speed" }, CorrectIndex = 1, Explanation = "80 PLUS ratings classify PSU energy efficiency levels." }
        }});

        // Lesson 42: Data Reliability and Recovery
        quizzes.Add(new Quiz { Id = qId++, LessonId = 42, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does RPO represent?", Options = new() { "Maximum acceptable data loss window", "Recovery server speed", "Power output of drives", "Network cable type" }, CorrectIndex = 0, Explanation = "RPO defines how much recent data loss is acceptable after an incident." },
            new() { Id = qId++, Text = "Why test restore procedures regularly?", Options = new() { "Backups are always perfect", "To verify recovery actually works", "To reduce storage usage only", "To disable encryption" }, CorrectIndex = 1, Explanation = "Testing restores ensures backup data is usable during real incidents." },
            new() { Id = qId++, Text = "Which practice strengthens data resilience?", Options = new() { "Single copy only", "Versioned backups and replication", "No access controls", "Disable monitoring" }, CorrectIndex = 1, Explanation = "Versioning and replication improve resilience across failure scenarios." }
        }});

        // Lesson 43: File Systems
        quizzes.Add(new Quiz { Id = qId++, LessonId = 43, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which file system is default on modern Windows installations?", Options = new() { "FAT32", "exFAT", "NTFS", "ext4" }, CorrectIndex = 2, Explanation = "NTFS is the default Windows file system, supporting large files, permissions, and journaling." },
            new() { Id = qId++, Text = "What does file system journaling protect against?", Options = new() { "Virus attacks", "Data corruption from unexpected shutdowns", "Slow read speeds", "Disk fragmentation" }, CorrectIndex = 1, Explanation = "Journaling logs changes before applying them, enabling recovery after unexpected shutdowns." },
            new() { Id = qId++, Text = "Which file system is best for a USB drive shared between Windows and macOS?", Options = new() { "NTFS", "APFS", "exFAT", "ext4" }, CorrectIndex = 2, Explanation = "exFAT supports large files and works natively on both Windows and macOS without third-party tools." }
        }});

        // Lesson 44: Input and Output Devices
        quizzes.Add(new Quiz { Id = qId++, LessonId = 44, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which of the following is both an input and output device?", Options = new() { "Printer", "Keyboard", "Touchscreen", "Speaker" }, CorrectIndex = 2, Explanation = "A touchscreen accepts touch input and displays visual output simultaneously." },
            new() { Id = qId++, Text = "Which monitor panel type is best for color accuracy?", Options = new() { "TN", "IPS", "CRT", "OLED" }, CorrectIndex = 1, Explanation = "IPS panels offer excellent color accuracy and wide viewing angles, favored by content creators." },
            new() { Id = qId++, Text = "What is the purpose of a screen reader?", Options = new() { "Scanning documents", "Displaying code", "Assisting visually impaired users by reading screen content aloud", "Capturing screenshots" }, CorrectIndex = 2, Explanation = "Screen readers are assistive technology that convert on-screen content to audio for visually impaired users." }
        }});

        // Lesson 45: Computer Networks Overview
        quizzes.Add(new Quiz { Id = qId++, LessonId = 45, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the basic unit of data transmitted over a network?", Options = new() { "File", "Packet", "Byte", "Frame" }, CorrectIndex = 1, Explanation = "Data is broken into packets that travel independently and are reassembled at the destination." },
            new() { Id = qId++, Text = "What device connects your home network to the internet?", Options = new() { "Switch", "Hub", "Router", "Repeater" }, CorrectIndex = 2, Explanation = "A router connects your local network to external networks including the internet." },
            new() { Id = qId++, Text = "What does TCP/IP govern?", Options = new() { "Monitor display", "Internet communication protocols", "File compression", "CPU scheduling" }, CorrectIndex = 1, Explanation = "TCP/IP is the suite of protocols governing how data is transmitted over the internet." }
        }});

        // Lesson 46: Software Types and Licenses
        quizzes.Add(new Quiz { Id = qId++, LessonId = 46, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which software type allows users to view and modify source code?", Options = new() { "Freeware", "Shareware", "Open-source", "Proprietary" }, CorrectIndex = 2, Explanation = "Open-source software makes source code available under licenses that permit viewing, modification, and redistribution." },
            new() { Id = qId++, Text = "What is SaaS?", Options = new() { "A hardware standard", "Software delivered and accessed over the internet", "A type of open-source license", "A programming language" }, CorrectIndex = 1, Explanation = "SaaS (Software as a Service) delivers software via the web, with no local installation required." },
            new() { Id = qId++, Text = "What category does an operating system belong to?", Options = new() { "Application software", "System software", "Middleware only", "Shareware" }, CorrectIndex = 1, Explanation = "Operating systems are system software that manage hardware and provide a platform for applications." }
        }});

        // Lesson 47: Binary and Number Systems
        quizzes.Add(new Quiz { Id = qId++, LessonId = 47, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "How many bits are in one byte?", Options = new() { "4", "6", "8", "16" }, CorrectIndex = 2, Explanation = "One byte consists of 8 bits and can represent 256 different values." },
            new() { Id = qId++, Text = "What is the decimal value of binary 1101?", Options = new() { "11", "12", "13", "14" }, CorrectIndex = 2, Explanation = "1101 in binary = 8+4+0+1 = 13 in decimal." },
            new() { Id = qId++, Text = "Hexadecimal (base-16) uses which letters alongside digits 0-9?", Options = new() { "G through P", "A through F", "A through Z", "X through Z" }, CorrectIndex = 1, Explanation = "Hexadecimal uses digits 0-9 and letters A-F, where A=10 and F=15." }
        }});

        // Lesson 48: Social Engineering Attacks
        quizzes.Add(new Quiz { Id = qId++, LessonId = 48, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What makes spear phishing different from regular phishing?", Options = new() { "It uses phone calls", "It targets specific individuals with personalized content", "It only attacks corporations", "It uses physical media" }, CorrectIndex = 1, Explanation = "Spear phishing crafts personalized messages targeting specific individuals, making them more convincing." },
            new() { Id = qId++, Text = "Which attack involves following an authorized person through a secure door?", Options = new() { "Phishing", "Tailgating", "Baiting", "Vishing" }, CorrectIndex = 1, Explanation = "Tailgating (piggybacking) is a physical social engineering attack exploiting courtesy to bypass physical security." },
            new() { Id = qId++, Text = "What is the primary defense against social engineering?", Options = new() { "Antivirus software", "Stronger firewalls", "Security awareness training for employees", "Encrypting all files" }, CorrectIndex = 2, Explanation = "Security awareness training is the most effective defense since social engineering targets human behavior." }
        }});

        // Lesson 49: Types of Malware
        quizzes.Add(new Quiz { Id = qId++, LessonId = 49, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which malware type encrypts victim files and demands payment?", Options = new() { "Worm", "Spyware", "Ransomware", "Rootkit" }, CorrectIndex = 2, Explanation = "Ransomware encrypts files and demands payment for the decryption key." },
            new() { Id = qId++, Text = "What distinguishes a worm from a virus?", Options = new() { "Worms need user action; viruses don't", "Worms self-replicate across networks without user action", "Viruses steal data; worms don't", "Worms are less harmful" }, CorrectIndex = 1, Explanation = "Worms self-propagate across networks without requiring user interaction, unlike viruses that attach to files." },
            new() { Id = qId++, Text = "What does a keylogger capture?", Options = new() { "Network packets", "Screen pixels", "Keystrokes to steal passwords", "Hard drive data" }, CorrectIndex = 2, Explanation = "Keyloggers record every keystroke, enabling attackers to capture passwords and sensitive information." }
        }});

        // Lesson 50: Network Security Tools
        quizzes.Add(new Quiz { Id = qId++, LessonId = 50, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does an IPS do that an IDS cannot?", Options = new() { "Detect attacks", "Log traffic", "Automatically block suspicious traffic", "Scan vulnerabilities" }, CorrectIndex = 2, Explanation = "IPS (Intrusion Prevention System) can automatically block threats, while IDS only detects and alerts." },
            new() { Id = qId++, Text = "Which tool is used to simulate attacks and find exploitable vulnerabilities?", Options = new() { "Antivirus scanner", "Penetration testing framework like Metasploit", "DHCP server", "Packet sniffer only" }, CorrectIndex = 1, Explanation = "Penetration testing frameworks like Metasploit simulate real attacks to find exploitable weaknesses." },
            new() { Id = qId++, Text = "What does SIEM stand for?", Options = new() { "Secure Internet Event Monitor", "Security Information and Event Management", "System Intrusion and Error Module", "Simulated Incident Event Manager" }, CorrectIndex = 1, Explanation = "SIEM (Security Information and Event Management) aggregates logs from multiple sources for real-time analysis." }
        }});

        // Lesson 51: Security Policies and Compliance
        quizzes.Add(new Quiz { Id = qId++, LessonId = 51, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which framework addresses payment card data security?", Options = new() { "HIPAA", "GDPR", "PCI DSS", "SOC 1" }, CorrectIndex = 2, Explanation = "PCI DSS (Payment Card Industry Data Security Standard) governs the security of cardholder data." },
            new() { Id = qId++, Text = "What is the purpose of a data classification policy?", Options = new() { "Define system uptime", "Label and govern how data is handled by sensitivity level", "Schedule software updates", "Manage user accounts" }, CorrectIndex = 1, Explanation = "Data classification policies assign sensitivity labels and define appropriate handling controls for each level." },
            new() { Id = qId++, Text = "What is the first step in risk management?", Options = new() { "Implement controls", "Write policies", "Identify assets and assess threats", "Train employees" }, CorrectIndex = 2, Explanation = "Risk management begins with identifying assets and assessing the threats and vulnerabilities that could affect them." }
        }});

        // Lesson 52: Incident Response
        quizzes.Add(new Quiz { Id = qId++, LessonId = 52, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the goal of the Containment phase in incident response?", Options = new() { "Delete all logs", "Identify the attacker's identity", "Limit the spread and damage of the incident", "Restore systems immediately" }, CorrectIndex = 2, Explanation = "Containment isolates affected systems to prevent the incident from spreading further." },
            new() { Id = qId++, Text = "What happens during the Eradication phase?", Options = new() { "Monitor for new threats", "Remove the root cause such as malware and compromised accounts", "Notify the press", "Back up data" }, CorrectIndex = 1, Explanation = "Eradication removes all traces of the attacker's presence, including malware and unauthorized accounts." },
            new() { Id = qId++, Text = "Why is the Lessons Learned phase important?", Options = new() { "It restores systems faster", "It improves defenses and prevents recurrence", "It assigns blame to individuals", "It reduces logging overhead" }, CorrectIndex = 1, Explanation = "Lessons Learned captures improvements to prevent recurrence and mature the organization's security posture." }
        }});

        // Lesson 53: Object-Oriented Programming
        quizzes.Add(new Quiz { Id = qId++, LessonId = 53, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which OOP pillar involves a child class inheriting from a parent class?", Options = new() { "Encapsulation", "Polymorphism", "Abstraction", "Inheritance" }, CorrectIndex = 3, Explanation = "Inheritance allows child classes to reuse attributes and methods from parent classes." },
            new() { Id = qId++, Text = "What is the purpose of __init__ in Python?", Options = new() { "Delete an object", "Import a module", "Initialize object attributes when an instance is created", "Define a class method" }, CorrectIndex = 2, Explanation = "__init__ is the constructor method called automatically when a new object is created." },
            new() { Id = qId++, Text = "What does encapsulation mean in OOP?", Options = new() { "Inheriting from multiple classes", "Overriding methods in subclasses", "Bundling data and methods and hiding internal details", "Creating multiple instances" }, CorrectIndex = 2, Explanation = "Encapsulation bundles data and methods into a class and restricts direct access to internal state." }
        }});

        // Lesson 54: File Handling in Python
        quizzes.Add(new Quiz { Id = qId++, LessonId = 54, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which file mode overwrites existing content when opening a file?", Options = new() { "'r'", "'a'", "'w'", "'rb'" }, CorrectIndex = 2, Explanation = "Mode 'w' opens a file for writing and overwrites any existing content." },
            new() { Id = qId++, Text = "What is the advantage of using the 'with' statement when opening files?", Options = new() { "Files are opened faster", "Files are automatically closed when the block exits", "Files are compressed", "It allows binary access only" }, CorrectIndex = 1, Explanation = "The 'with' statement ensures the file is automatically closed even if an exception occurs." },
            new() { Id = qId++, Text = "Which method reads all lines of a file into a list?", Options = new() { "f.read()", "f.readline()", "f.readlines()", "f.readall()" }, CorrectIndex = 2, Explanation = "f.readlines() reads all lines and returns them as a list of strings." }
        }});

        // Lesson 55: Error Handling and Exceptions
        quizzes.Add(new Quiz { Id = qId++, LessonId = 55, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which clause in a try/except block always executes?", Options = new() { "except", "else", "try", "finally" }, CorrectIndex = 3, Explanation = "The finally clause always executes regardless of whether an exception occurred, making it ideal for cleanup." },
            new() { Id = qId++, Text = "What exception is raised when a dictionary key does not exist?", Options = new() { "ValueError", "TypeError", "KeyError", "IndexError" }, CorrectIndex = 2, Explanation = "KeyError is raised when a key is not found in a dictionary." },
            new() { Id = qId++, Text = "How do you intentionally trigger an exception in Python?", Options = new() { "throw ValueError('msg')", "raise ValueError('msg')", "except ValueError('msg')", "trigger ValueError('msg')" }, CorrectIndex = 1, Explanation = "Python uses the 'raise' keyword to raise an exception explicitly." }
        }});

        // Lesson 56: Working with Libraries and pip
        quizzes.Add(new Quiz { Id = qId++, LessonId = 56, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the purpose of a Python virtual environment?", Options = new() { "Speed up code execution", "Isolate project dependencies to avoid version conflicts", "Compress Python files", "Connect to the internet" }, CorrectIndex = 1, Explanation = "Virtual environments isolate each project's packages, preventing conflicts between different projects." },
            new() { Id = qId++, Text = "Which command saves all installed packages to a text file?", Options = new() { "pip list --save", "pip export requirements.txt", "pip freeze > requirements.txt", "pip save > requirements.txt" }, CorrectIndex = 2, Explanation = "pip freeze outputs installed packages and versions; redirecting to requirements.txt saves the environment snapshot." },
            new() { Id = qId++, Text = "Which library is commonly used for HTTP requests in Python?", Options = new() { "os", "sys", "requests", "json" }, CorrectIndex = 2, Explanation = "The 'requests' library is the most popular Python library for making HTTP requests." }
        }});

        // Lesson 57: Debugging and Testing
        quizzes.Add(new Quiz { Id = qId++, LessonId = 57, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does Test-Driven Development (TDD) require?", Options = new() { "Writing tests after the code is complete", "Writing tests before writing the code", "Skipping unit tests for speed", "Only testing production code" }, CorrectIndex = 1, Explanation = "TDD writes tests first to define expected behavior, then implements code to pass those tests." },
            new() { Id = qId++, Text = "Which Python testing framework is most popular for its concise syntax?", Options = new() { "unittest", "pytest", "doctest", "mock" }, CorrectIndex = 1, Explanation = "pytest is the most widely used Python testing framework, known for its simple syntax and powerful plugins." },
            new() { Id = qId++, Text = "What does code coverage measure?", Options = new() { "Code execution speed", "Memory usage", "Percentage of code exercised by tests", "Number of bugs found" }, CorrectIndex = 2, Explanation = "Code coverage measures what percentage of source code is executed during test runs." }
        }});

        // Lesson 58: JavaScript DOM Manipulation
        quizzes.Add(new Quiz { Id = qId++, LessonId = 58, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which method selects the first matching HTML element by CSS selector?", Options = new() { "getElementById", "querySelector", "getElementsByClassName", "getElement" }, CorrectIndex = 1, Explanation = "querySelector returns the first element matching a CSS selector." },
            new() { Id = qId++, Text = "What does event delegation accomplish?", Options = new() { "Prevents all events from firing", "Attaches one listener to a parent to handle events from children", "Removes event listeners automatically", "Fires events in reverse order" }, CorrectIndex = 1, Explanation = "Event delegation attaches one listener to a parent element instead of many listeners on individual children." },
            new() { Id = qId++, Text = "Which property changes the visible text content of an element safely?", Options = new() { "innerHTML", "outerHTML", "textContent", "elementText" }, CorrectIndex = 2, Explanation = "textContent sets plain text content safely, without parsing HTML or risking XSS injection." }
        }});

        // Lesson 59: Responsive Web Design
        quizzes.Add(new Quiz { Id = qId++, LessonId = 59, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which CSS property defines a flex container?", Options = new() { "position: flex", "display: flex", "float: flex", "layout: flex" }, CorrectIndex = 1, Explanation = "display: flex creates a flex container, enabling Flexbox layout for its children." },
            new() { Id = qId++, Text = "What does a CSS media query do?", Options = new() { "Queries a database", "Fetches images dynamically", "Applies different styles based on screen size or device", "Adds video content" }, CorrectIndex = 2, Explanation = "Media queries apply CSS rules conditionally based on screen width, height, resolution, or other features." },
            new() { Id = qId++, Text = "Why is the viewport meta tag important for responsive design?", Options = new() { "It speeds up page loading", "It prevents mobile browsers from scaling down desktop-sized layouts", "It adds touch events", "It compresses CSS" }, CorrectIndex = 1, Explanation = "The viewport meta tag ensures mobile browsers render at the device's native width rather than scaling down a desktop layout." }
        }});

        // Lesson 60: Web APIs and the Fetch API
        quizzes.Add(new Quiz { Id = qId++, LessonId = 60, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which HTTP method is used to create a new resource in a REST API?", Options = new() { "GET", "DELETE", "POST", "HEAD" }, CorrectIndex = 2, Explanation = "POST creates a new resource on the server and typically returns 201 Created on success." },
            new() { Id = qId++, Text = "What HTTP status code indicates the client sent a bad request?", Options = new() { "200", "404", "500", "400" }, CorrectIndex = 3, Explanation = "400 Bad Request indicates the server could not process the request due to a client error." },
            new() { Id = qId++, Text = "Why should API secret keys never be placed in client-side JavaScript?", Options = new() { "JavaScript cannot store strings", "Anyone viewing the page source can read and misuse them", "Keys only work server-side", "JavaScript encrypts them automatically" }, CorrectIndex = 1, Explanation = "Client-side JavaScript is visible to anyone who views the page source, exposing any embedded secrets." }
        }});

        // Lesson 61: Version Control with Git
        quizzes.Add(new Quiz { Id = qId++, LessonId = 61, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does 'git add .' do?", Options = new() { "Deletes all files", "Creates a new branch", "Stages all changed files for the next commit", "Pushes changes to remote" }, CorrectIndex = 2, Explanation = "git add . stages all changes in the current directory for inclusion in the next commit." },
            new() { Id = qId++, Text = "What is the purpose of a pull request?", Options = new() { "Download code from a server", "Request code review before merging a branch", "Pull the latest commits locally", "Delete a remote branch" }, CorrectIndex = 1, Explanation = "A pull request asks collaborators to review code changes in a branch before merging it into the main branch." },
            new() { Id = qId++, Text = "What file tells Git which files and folders to ignore?", Options = new() { ".gitconfig", ".gitattributes", ".gitignore", ".gitkeep" }, CorrectIndex = 2, Explanation = ".gitignore lists patterns of files Git should not track, such as build artifacts and secrets." }
        }});

        // Lesson 62: Web Accessibility and Performance
        quizzes.Add(new Quiz { Id = qId++, LessonId = 62, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the minimum color contrast ratio recommended by WCAG for body text?", Options = new() { "2:1", "3:1", "4.5:1", "7:1" }, CorrectIndex = 2, Explanation = "WCAG AA requires a 4.5:1 contrast ratio for normal text to ensure readability for users with low vision." },
            new() { Id = qId++, Text = "Which HTML element should be used for clickable buttons?", Options = new() { "<div>", "<span>", "<a>", "<button>" }, CorrectIndex = 3, Explanation = "Using <button> provides built-in accessibility: keyboard focus, Enter/Space activation, and correct ARIA role." },
            new() { Id = qId++, Text = "What does LCP measure in Core Web Vitals?", Options = new() { "Link Click Performance", "Largest Contentful Paint — time until the largest visible element is rendered", "Lowest Content Priority", "Last Click Position" }, CorrectIndex = 1, Explanation = "LCP (Largest Contentful Paint) measures how long the largest visible content element takes to render." }
        }});

        // Lesson 63: Articles and Determiners
        quizzes.Add(new Quiz { Id = qId++, LessonId = 63, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which article is used before words beginning with a vowel sound?", Options = new() { "a", "an", "the", "some" }, CorrectIndex = 1, Explanation = "'An' is used before vowel sounds (an apple, an hour). The rule is about sound, not spelling." },
            new() { Id = qId++, Text = "Which sentence uses the zero article correctly?", Options = new() { "I love the music.", "A honesty is important.", "Water is essential for life.", "She saw the dogs." }, CorrectIndex = 2, Explanation = "Uncountable nouns used in a general sense (Water, Music, Honesty) take no article." },
            new() { Id = qId++, Text = "When should 'the' be used?", Options = new() { "Before any noun", "When introducing a noun for the first time", "When referring to a specific, already known noun", "Only before plural nouns" }, CorrectIndex = 2, Explanation = "'The' is the definite article used when both speaker and listener know which specific noun is meant." }
        }});

        // Lesson 64: Modal Verbs
        quizzes.Add(new Quiz { Id = qId++, LessonId = 64, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which modal expresses strong logical certainty about the present?", Options = new() { "might", "could", "must", "should" }, CorrectIndex = 2, Explanation = "'Must' expresses logical deduction: 'He must be tired' means you are nearly certain he is tired." },
            new() { Id = qId++, Text = "What does 'should have studied' express?", Options = new() { "A completed action", "A future plan", "A past regret or criticism", "Permission" }, CorrectIndex = 2, Explanation = "Perfect modals like 'should have + past participle' refer to past situations, often expressing regret." },
            new() { Id = qId++, Text = "Modals are followed by:", Options = new() { "The infinitive with 'to'", "The base form of the verb without 'to'", "The gerund (-ing form)", "A past participle" }, CorrectIndex = 1, Explanation = "Modal verbs (can, must, should, etc.) are always followed by the base infinitive without 'to'." }
        }});

        // Lesson 65: Conditionals
        quizzes.Add(new Quiz { Id = qId++, LessonId = 65, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which conditional describes a hypothetical past situation?", Options = new() { "Zero conditional", "First conditional", "Second conditional", "Third conditional" }, CorrectIndex = 3, Explanation = "Third conditional (If + past perfect, would have + past participle) describes unreal past situations." },
            new() { Id = qId++, Text = "What tense is used in the condition clause of the second conditional?", Options = new() { "Present simple", "Past simple", "Past perfect", "Future simple" }, CorrectIndex = 1, Explanation = "Second conditional uses past simple in the condition clause to signal that the situation is unreal or hypothetical." },
            new() { Id = qId++, Text = "Which conditional is used for general truths and scientific facts?", Options = new() { "Zero conditional", "First conditional", "Second conditional", "Mixed conditional" }, CorrectIndex = 0, Explanation = "Zero conditional (If/When + present, present) describes universal truths: 'If you heat ice, it melts.'" }
        }});

        // Lesson 66: Passive Voice
        quizzes.Add(new Quiz { Id = qId++, LessonId = 66, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "How is the passive voice formed?", Options = new() { "Subject + have + verb", "To be + past participle", "To have + been + adjective", "Subject + will + infinitive" }, CorrectIndex = 1, Explanation = "The passive is formed using an appropriate tense of 'be' followed by the past participle." },
            new() { Id = qId++, Text = "When is passive voice most appropriate?", Options = new() { "When you want to sound casual", "When the doer is unknown or unimportant", "When writing dialogue", "When describing future plans" }, CorrectIndex = 1, Explanation = "Passive voice is appropriate when the doer is unknown, irrelevant, or when emphasizing the result matters more." },
            new() { Id = qId++, Text = "What is the passive form of 'They are building a new hospital'?", Options = new() { "A new hospital was built", "A new hospital is being built", "A new hospital will be built", "A new hospital has been built" }, CorrectIndex = 1, Explanation = "Present continuous passive: to be (is/are) + being + past participle." }
        }});

        // Lesson 67: Reported Speech
        quizzes.Add(new Quiz { Id = qId++, LessonId = 67, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What tense shift occurs when reporting 'I am happy'?", Options = new() { "Stays the same", "Becomes 'I was happy'", "Becomes 'I will be happy'", "Becomes 'I have been happy'" }, CorrectIndex = 1, Explanation = "Present simple shifts to past simple in reported speech: 'I am happy' → She said she was happy." },
            new() { Id = qId++, Text = "Which word introduces a reported yes/no question?", Options = new() { "that", "what", "if/whether", "which" }, CorrectIndex = 2, Explanation = "Reported yes/no questions use 'if' or 'whether': 'Are you ready?' → She asked if I was ready." },
            new() { Id = qId++, Text = "What is the difference between 'say' and 'tell' in reported speech?", Options = new() { "No difference", "'Tell' requires a person object; 'say' does not", "'Say' is only for questions", "'Tell' is only used in formal writing" }, CorrectIndex = 1, Explanation = "'Tell' requires a person object (He told me), while 'say' does not (He said he was late)." }
        }});

        // Lesson 68: Spanish Pronouns
        quizzes.Add(new Quiz { Id = qId++, LessonId = 68, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the formal 'you' singular in Spanish?", Options = new() { "tú", "vosotros", "usted", "ustedes" }, CorrectIndex = 2, Explanation = "'Usted' is the formal singular 'you' in Spanish, used in professional or respectful contexts." },
            new() { Id = qId++, Text = "Where do object pronouns typically appear relative to conjugated verbs?", Options = new() { "After the verb", "Before the verb", "At the end of the sentence", "Before the subject" }, CorrectIndex = 1, Explanation = "Object pronouns generally precede conjugated verbs in Spanish: Lo veo (I see it)." },
            new() { Id = qId++, Text = "Which pronoun is used with reflexive verbs in the third person?", Options = new() { "me", "te", "se", "le" }, CorrectIndex = 2, Explanation = "'Se' is the reflexive pronoun for él/ella/usted/ellos/ellas: Se llama María (Her name is María)." }
        }});

        // Lesson 69: Present Tense Conjugation
        quizzes.Add(new Quiz { Id = qId++, LessonId = 69, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the yo form of the irregular verb 'tener'?", Options = new() { "teno", "tengo", "tiene", "tenga" }, CorrectIndex = 1, Explanation = "Tener is irregular in the yo form: tengo (I have)." },
            new() { Id = qId++, Text = "Which stem change occurs in the verb 'querer' (to want)?", Options = new() { "o → ue", "e → ie", "e → i", "u → ue" }, CorrectIndex = 1, Explanation = "Querer is an e→ie stem-changing verb: quiero, quieres, quiere (but queremos, queréis)." },
            new() { Id = qId++, Text = "What is the present tense yo form of 'hablar'?", Options = new() { "hable", "hablas", "hablo", "hablan" }, CorrectIndex = 2, Explanation = "Regular -ar verbs in the yo form end in -o: hablo (I speak)." }
        }});

        // Lesson 70: Telling Time in Spanish
        quizzes.Add(new Quiz { Id = qId++, LessonId = 70, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "How do you say 'It is three o'clock' in Spanish?", Options = new() { "Es la tres.", "Son las tres.", "Está las tres.", "Hay las tres." }, CorrectIndex = 1, Explanation = "'Son las' is used for all hours except one o'clock: Son las tres." },
            new() { Id = qId++, Text = "How do you express 'It is 6:30' in Spanish?", Options = new() { "Son las seis y cuarto.", "Son las seis y media.", "Son las seis menos media.", "Es la seis y media." }, CorrectIndex = 1, Explanation = "'Y media' means 'and a half': Son las seis y media (It's 6:30)." },
            new() { Id = qId++, Text = "How do you ask 'At what time?' in Spanish?", Options = new() { "¿Qué hora es?", "¿Cuándo es?", "¿A qué hora?", "¿Cómo es la hora?" }, CorrectIndex = 2, Explanation = "¿A qué hora? means 'At what time?' — used when asking when something happens." }
        }});

        // Lesson 71: Spanish Prepositions
        quizzes.Add(new Quiz { Id = qId++, LessonId = 71, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the contraction of 'de + el' in Spanish?", Options = new() { "del", "de el", "al", "del'" }, CorrectIndex = 0, Explanation = "'De + el' always contracts to 'del': Vengo del trabajo (I come from work)." },
            new() { Id = qId++, Text = "Which preposition expresses purpose or destination?", Options = new() { "por", "de", "en", "para" }, CorrectIndex = 3, Explanation = "'Para' expresses purpose (in order to) and destination: Estudio para aprender (I study to learn)." },
            new() { Id = qId++, Text = "How do you say 'next to' in Spanish?", Options = new() { "encima de", "detrás de", "al lado de", "debajo de" }, CorrectIndex = 2, Explanation = "'Al lado de' means 'next to' or 'beside' in Spanish." }
        }});

        // Lesson 72: Food and Restaurant Vocabulary
        quizzes.Add(new Quiz { Id = qId++, LessonId = 72, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "How do you say 'The bill, please' in Spanish?", Options = new() { "El menú, por favor.", "La cuenta, por favor.", "El plato, por favor.", "La factura, por favor." }, CorrectIndex = 1, Explanation = "'La cuenta, por favor' is the standard way to ask for the bill at a restaurant." },
            new() { Id = qId++, Text = "What does 'picante' mean in Spanish?", Options = new() { "Sweet", "Salty", "Spicy", "Bitter" }, CorrectIndex = 2, Explanation = "'Picante' means spicy or hot (referring to spiciness, not temperature)." },
            new() { Id = qId++, Text = "How would you tell a waiter you are vegetarian?", Options = new() { "No quiero comer.", "Soy vegetariano/a.", "No hay carne aquí.", "Tengo hambre." }, CorrectIndex = 1, Explanation = "'Soy vegetariano/a' (I am vegetarian) informs restaurant staff of your dietary preference." }
        }});

        // Lesson 73: Network Devices Deep Dive
        quizzes.Add(new Quiz { Id = qId++, LessonId = 73, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What key difference distinguishes a switch from a hub?", Options = new() { "Switches are wireless; hubs are wired", "Switches forward frames only to the intended recipient; hubs broadcast to all ports", "Hubs are faster", "Switches require manual configuration for all devices" }, CorrectIndex = 1, Explanation = "Switches use MAC address tables to direct frames to specific ports, while hubs broadcast to all ports." },
            new() { Id = qId++, Text = "At which OSI layer does a router operate?", Options = new() { "Layer 1 (Physical)", "Layer 2 (Data Link)", "Layer 3 (Network)", "Layer 7 (Application)" }, CorrectIndex = 2, Explanation = "Routers operate at Layer 3 (Network layer), forwarding packets based on IP addresses." },
            new() { Id = qId++, Text = "What does a proxy server provide?", Options = new() { "Only DNS resolution", "Physical network connections", "Caching, filtering, and acting as intermediary between clients and internet", "Wireless access points" }, CorrectIndex = 2, Explanation = "Proxy servers act as intermediaries, providing caching, content filtering, and potentially anonymity for clients." }
        }});

        // Lesson 74: Wireless Networking
        quizzes.Add(new Quiz { Id = qId++, LessonId = 74, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the current Wi-Fi security standard?", Options = new() { "WEP", "WPA", "WPA2", "WPA3" }, CorrectIndex = 3, Explanation = "WPA3 is the current Wi-Fi security standard with stronger encryption and forward secrecy." },
            new() { Id = qId++, Text = "Which frequency band offers better range and wall penetration?", Options = new() { "5 GHz", "6 GHz", "2.4 GHz", "60 GHz" }, CorrectIndex = 2, Explanation = "2.4 GHz travels farther and penetrates walls better than 5 GHz, though with less bandwidth." },
            new() { Id = qId++, Text = "What Wi-Fi generation is 802.11ax?", Options = new() { "Wi-Fi 4", "Wi-Fi 5", "Wi-Fi 6", "Wi-Fi 7" }, CorrectIndex = 2, Explanation = "802.11ax is Wi-Fi 6, offering higher throughput and improved performance in dense environments." }
        }});

        // Lesson 75: DNS and DHCP
        quizzes.Add(new Quiz { Id = qId++, LessonId = 75, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which DNS record type maps a domain name to an IPv4 address?", Options = new() { "MX", "CNAME", "A", "TXT" }, CorrectIndex = 2, Explanation = "An A record maps a hostname to its IPv4 address." },
            new() { Id = qId++, Text = "What does DORA stand for in the DHCP process?", Options = new() { "Detect, Operate, Report, Assign", "Discover, Offer, Request, Acknowledge", "Define, Order, Route, Assign", "Delegate, Observe, Respond, Approve" }, CorrectIndex = 1, Explanation = "DORA describes the DHCP handshake: Discover → Offer → Request → Acknowledge." },
            new() { Id = qId++, Text = "What IP address range indicates a DHCP failure on Windows?", Options = new() { "192.168.x.x", "10.0.x.x", "169.254.x.x", "172.16.x.x" }, CorrectIndex = 2, Explanation = "169.254.x.x (APIPA) addresses are self-assigned when no DHCP server responds, indicating a configuration problem." }
        }});

        // Lesson 76: VPNs and Remote Access
        quizzes.Add(new Quiz { Id = qId++, LessonId = 76, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does a VPN primarily protect?", Options = new() { "Data at rest on your hard drive", "Data in transit over the network", "Your device from malware", "Passwords stored in browsers" }, CorrectIndex = 1, Explanation = "VPNs encrypt network traffic in transit, preventing eavesdropping on public or untrusted networks." },
            new() { Id = qId++, Text = "What is split tunneling in a VPN?", Options = new() { "Using two VPN servers simultaneously", "Routing only some traffic through the VPN while other traffic goes directly", "Breaking an encryption key in half", "Splitting bandwidth between users" }, CorrectIndex = 1, Explanation = "Split tunneling routes only specific traffic (e.g., corporate) through the VPN, reducing bandwidth overhead." },
            new() { Id = qId++, Text = "Which modern VPN protocol is known for speed and a minimal codebase?", Options = new() { "PPTP", "L2TP", "WireGuard", "OpenVPN" }, CorrectIndex = 2, Explanation = "WireGuard is a modern VPN protocol with a small, auditable codebase and excellent performance." }
        }});

        // Lesson 77: Network Monitoring and Troubleshooting
        quizzes.Add(new Quiz { Id = qId++, LessonId = 77, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does the traceroute/tracert command show?", Options = new() { "All DNS records", "Active TCP connections", "The path packets take and per-hop latency to a destination", "Open ports on a server" }, CorrectIndex = 2, Explanation = "Traceroute maps the path packets follow and measures latency at each hop, revealing where delays or failures occur." },
            new() { Id = qId++, Text = "Where should network troubleshooting start in the OSI model?", Options = new() { "Layer 7 (Application)", "Layer 4 (Transport)", "Layer 1 (Physical)", "Layer 3 (Network)" }, CorrectIndex = 2, Explanation = "Troubleshooting starts at Layer 1 (physical connections, cables, link lights) and works up through the layers." },
            new() { Id = qId++, Text = "Which tool captures and analyzes individual network packets?", Options = new() { "ping", "netstat", "Wireshark", "nslookup" }, CorrectIndex = 2, Explanation = "Wireshark is a packet capture and analysis tool that lets you examine individual packets at every protocol layer." }
        }});

        // Lesson 78: HTTP and HTTPS In Depth
        quizzes.Add(new Quiz { Id = qId++, LessonId = 78, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What HTTP status code means the resource was not found?", Options = new() { "200", "301", "403", "404" }, CorrectIndex = 3, Explanation = "404 Not Found indicates the requested resource does not exist on the server." },
            new() { Id = qId++, Text = "What does HTTPS add to HTTP?", Options = new() { "Faster loading", "TLS encryption for confidentiality and integrity", "Larger file transfers", "Built-in caching" }, CorrectIndex = 1, Explanation = "HTTPS adds TLS encryption, preventing eavesdropping, tampering, and impersonation." },
            new() { Id = qId++, Text = "What improvement does HTTP/2 bring over HTTP/1.1?", Options = new() { "Uses UDP instead of TCP", "Multiplexing multiple requests over a single connection", "Removes the need for HTTPS", "Requires larger headers" }, CorrectIndex = 1, Explanation = "HTTP/2 introduces multiplexing, allowing multiple concurrent requests over one connection, reducing latency." }
        }});

        // Lesson 79: DNS Protocol In Depth
        quizzes.Add(new Quiz { Id = qId++, LessonId = 79, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the role of the recursive resolver in DNS?", Options = new() { "Store authoritative zone records", "Perform iterative queries on behalf of the client to resolve a name", "Block malicious domains", "Assign IP addresses" }, CorrectIndex = 1, Explanation = "The recursive resolver queries root, TLD, and authoritative servers iteratively to find the answer for the client." },
            new() { Id = qId++, Text = "What does DNSSEC add to DNS?", Options = new() { "Faster resolution", "Encryption of DNS queries", "Digital signatures to verify record authenticity", "DHCP integration" }, CorrectIndex = 2, Explanation = "DNSSEC adds cryptographic signatures to DNS records, allowing clients to verify they haven't been tampered with." },
            new() { Id = qId++, Text = "What does TTL (Time to Live) control in DNS?", Options = new() { "Network cable lifespan", "How long a DNS record is cached before re-querying", "VPN tunnel duration", "TLS certificate validity" }, CorrectIndex = 1, Explanation = "TTL specifies how long resolvers and clients cache a DNS record before querying the authoritative server again." }
        }});

        // Lesson 80: DHCP Protocol
        quizzes.Add(new Quiz { Id = qId++, LessonId = 80, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the first message a DHCP client sends when joining a network?", Options = new() { "DHCPREQUEST", "DHCPACK", "DHCPDISCOVER", "DHCPOFFER" }, CorrectIndex = 2, Explanation = "DHCPDISCOVER is the broadcast message a client sends to find available DHCP servers." },
            new() { Id = qId++, Text = "What is a DHCP reservation?", Options = new() { "A range of excluded IP addresses", "Binding a specific IP to a device's MAC address", "A list of DNS servers", "A temporary IP lease" }, CorrectIndex = 1, Explanation = "DHCP reservations bind a specific IP address to a device's MAC address so it always gets the same IP." },
            new() { Id = qId++, Text = "What security feature blocks rogue DHCP servers on managed switches?", Options = new() { "Port security", "DHCP snooping", "802.1X", "VLAN tagging" }, CorrectIndex = 1, Explanation = "DHCP snooping allows only designated trusted ports to send DHCP responses, blocking rogue DHCP servers." }
        }});

        // Lesson 81: Email Protocols
        quizzes.Add(new Quiz { Id = qId++, LessonId = 81, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which protocol sends outgoing email from client to server?", Options = new() { "IMAP", "POP3", "SMTP", "FTP" }, CorrectIndex = 2, Explanation = "SMTP (Simple Mail Transfer Protocol) handles sending email from client to server and between servers." },
            new() { Id = qId++, Text = "What advantage does IMAP have over POP3?", Options = new() { "IMAP is faster", "IMAP keeps email on the server and syncs across multiple devices", "POP3 uses less bandwidth", "POP3 supports folders" }, CorrectIndex = 1, Explanation = "IMAP stores email on the server, enabling access and synchronization across multiple devices." },
            new() { Id = qId++, Text = "What does DKIM add to email messages?", Options = new() { "Delivery priority flags", "A cryptographic signature to verify authenticity", "End-to-end encryption", "Spam filtering rules" }, CorrectIndex = 1, Explanation = "DKIM adds a digital signature to email headers and body, allowing recipients to verify the message wasn't tampered with." }
        }});

        // Lesson 82: Network Troubleshooting Tools
        quizzes.Add(new Quiz { Id = qId++, LessonId = 82, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which command tests basic network reachability and measures round-trip time?", Options = new() { "tracert", "netstat", "ping", "nslookup" }, CorrectIndex = 2, Explanation = "Ping sends ICMP echo requests to test if a host is reachable and measures round-trip latency." },
            new() { Id = qId++, Text = "What does 'netstat' display?", Options = new() { "DNS cache", "Active network connections and listening ports", "Wireless signal strength", "IP routing table" }, CorrectIndex = 1, Explanation = "Netstat (and its modern replacement 'ss') shows active TCP/UDP connections and ports a service is listening on." },
            new() { Id = qId++, Text = "Which tool is best for testing HTTP/S connectivity to a specific web endpoint?", Options = new() { "ping", "tracert", "arp", "curl" }, CorrectIndex = 3, Explanation = "curl makes HTTP/S requests and displays response headers and body, ideal for testing web endpoints and APIs." }
        }});

        // Lesson 83: Feature Engineering
        quizzes.Add(new Quiz { Id = qId++, LessonId = 83, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Why is feature scaling important for algorithms like SVM and KNN?", Options = new() { "It speeds up data loading", "These algorithms are sensitive to the relative scale of features", "It removes missing values", "It improves model interpretability" }, CorrectIndex = 1, Explanation = "SVM, KNN, and gradient-based algorithms are sensitive to feature scales; unscaled features can dominate distance calculations." },
            new() { Id = qId++, Text = "What does one-hot encoding do to a categorical variable?", Options = new() { "Assigns sequential integers", "Creates a binary column for each category", "Normalizes values to 0-1", "Removes the variable" }, CorrectIndex = 1, Explanation = "One-hot encoding converts each category into a binary (0/1) indicator column, avoiding implied ordinal relationships." },
            new() { Id = qId++, Text = "What is L1 regularization's effect on irrelevant features?", Options = new() { "Increases their weights", "Sets their weights to exactly zero", "Doubles their weights", "Converts them to binary" }, CorrectIndex = 1, Explanation = "L1 (Lasso) regularization can shrink coefficients of irrelevant features to exactly zero, effectively removing them." }
        }});

        // Lesson 84: Unsupervised Learning
        quizzes.Add(new Quiz { Id = qId++, LessonId = 84, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is K-Means clustering's primary goal?", Options = new() { "Predict labels for new data", "Minimize within-cluster variance by assigning points to k centroids", "Maximize distance between all points", "Find the best decision boundary" }, CorrectIndex = 1, Explanation = "K-Means minimizes within-cluster sum of squared distances by iteratively assigning points to the nearest centroid." },
            new() { Id = qId++, Text = "What does PCA (Principal Component Analysis) achieve?", Options = new() { "Classifies data into groups", "Reduces dimensions while preserving maximum variance", "Removes duplicate records", "Increases model accuracy" }, CorrectIndex = 1, Explanation = "PCA projects data onto principal components (directions of maximum variance), reducing dimensionality." },
            new() { Id = qId++, Text = "What is anomaly detection used for in cybersecurity?", Options = new() { "Training classifiers", "Identifying network traffic that deviates from normal patterns", "Generating synthetic data", "Compressing log files" }, CorrectIndex = 1, Explanation = "Anomaly detection identifies unusual patterns that may indicate network intrusions or compromised systems." }
        }});

        // Lesson 85: Reinforcement Learning
        quizzes.Add(new Quiz { Id = qId++, LessonId = 85, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What guides an RL agent's decision-making?", Options = new() { "A labeled training set", "A policy mapping states to actions", "Predefined expert rules", "A regression formula" }, CorrectIndex = 1, Explanation = "The policy defines how the agent selects actions in each state to maximize cumulative reward." },
            new() { Id = qId++, Text = "What problem arises when an RL agent only exploits known good actions?", Options = new() { "It learns too fast", "It may miss better strategies it hasn't yet explored", "It overfits the reward function", "It requires more memory" }, CorrectIndex = 1, Explanation = "Pure exploitation without exploration may converge to suboptimal strategies by missing better undiscovered actions." },
            new() { Id = qId++, Text = "What did AlphaGo demonstrate about reinforcement learning?", Options = new() { "RL cannot solve complex games", "RL can achieve superhuman performance in complex strategy games like Go", "RL only works on simple tasks", "RL requires supervised pre-training only" }, CorrectIndex = 1, Explanation = "AlphaGo used RL to achieve superhuman performance at the game of Go, a milestone for AI." }
        }});

        // Lesson 86: Model Deployment
        quizzes.Add(new Quiz { Id = qId++, LessonId = 86, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the purpose of containerizing an ML model with Docker?", Options = new() { "Speed up training", "Ensure a consistent, reproducible environment between development and production", "Store training data", "Replace GPU acceleration" }, CorrectIndex = 1, Explanation = "Docker containers package the model with all dependencies, ensuring consistent behavior across different environments." },
            new() { Id = qId++, Text = "What is model drift?", Options = new() { "A model's accuracy improving over time", "A model's performance degrading as real-world data patterns change", "A model becoming too large to serve", "A training error" }, CorrectIndex = 1, Explanation = "Model drift occurs when the real-world data distribution shifts from what the model was trained on, degrading performance." },
            new() { Id = qId++, Text = "What does A/B testing enable in model deployment?", Options = new() { "Training two models simultaneously", "Comparing a new model version against the current production model on live traffic", "Testing on synthetic data only", "Encrypting model weights" }, CorrectIndex = 1, Explanation = "A/B testing exposes different users to different model versions to compare their real-world performance." }
        }});

        // Lesson 87: Ethics in AI
        quizzes.Add(new Quiz { Id = qId++, LessonId = 87, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is a primary source of bias in AI models?", Options = new() { "Too many training iterations", "Biased or unrepresentative training data", "Using GPUs for training", "High model accuracy" }, CorrectIndex = 1, Explanation = "Models trained on biased data learn and amplify those biases in their predictions." },
            new() { Id = qId++, Text = "What do SHAP and LIME provide?", Options = new() { "Faster model training", "More training data", "Explanations of individual model predictions", "Data compression" }, CorrectIndex = 2, Explanation = "SHAP and LIME are explainability tools that explain why a model made a specific prediction for a given input." },
            new() { Id = qId++, Text = "What does differential privacy protect against?", Options = new() { "SQL injection attacks", "Overfitting", "Reconstruction of individual training data from model outputs", "Network intrusions" }, CorrectIndex = 2, Explanation = "Differential privacy adds noise to model training, limiting how much can be inferred about individual training examples." }
        }});

        // Lesson 88: Convolutional Neural Networks
        quizzes.Add(new Quiz { Id = qId++, LessonId = 88, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does a convolution operation do in a CNN?", Options = new() { "Reduces the number of classes", "Slides a filter over the input to detect local patterns like edges and textures", "Normalizes pixel values to 0-1", "Connects every neuron to every other" }, CorrectIndex = 1, Explanation = "Convolution slides learnable filters over the input, producing feature maps that detect local patterns." },
            new() { Id = qId++, Text = "What is the purpose of max pooling?", Options = new() { "Increase spatial dimensions", "Add more filters", "Reduce spatial dimensions while retaining the strongest features", "Apply batch normalization" }, CorrectIndex = 2, Explanation = "Max pooling downsamples feature maps by taking the maximum value in each window, reducing computation and adding invariance." },
            new() { Id = qId++, Text = "What innovation in ResNet enabled training very deep CNNs?", Options = new() { "Larger filters", "Residual (skip) connections that allow gradients to flow past layers", "Removing pooling layers", "Using only fully connected layers" }, CorrectIndex = 1, Explanation = "Residual connections in ResNet allow gradients to bypass layers, solving the vanishing gradient problem for very deep networks." }
        }});

        // Lesson 89: Recurrent Neural Networks and LSTMs
        quizzes.Add(new Quiz { Id = qId++, LessonId = 89, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What problem do LSTMs solve compared to vanilla RNNs?", Options = new() { "Slow training speed", "Inability to process text", "Vanishing gradients that cause loss of information over long sequences", "Excessive memory use" }, CorrectIndex = 2, Explanation = "LSTMs use gates to control information flow, preventing vanishing gradients over long sequences." },
            new() { Id = qId++, Text = "What do bidirectional RNNs capture that unidirectional RNNs miss?", Options = new() { "Higher learning rates", "Context from both past and future positions in a sequence", "Convolutional features", "Static feature vectors" }, CorrectIndex = 1, Explanation = "Bidirectional RNNs process sequences forward and backward, capturing context from both directions." },
            new() { Id = qId++, Text = "For which data type are RNNs and LSTMs naturally suited?", Options = new() { "Static images", "Sequential data like text, time series, and audio", "Tabular structured data", "Spatial 3D data" }, CorrectIndex = 1, Explanation = "RNNs and LSTMs are designed for sequential data where order and temporal dependencies matter." }
        }});

        // Lesson 90: Transformers and Attention Mechanisms
        quizzes.Add(new Quiz { Id = qId++, LessonId = 90, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What key mechanism replaces recurrence in the Transformer architecture?", Options = new() { "Convolutions", "Self-attention", "Pooling layers", "Dropout" }, CorrectIndex = 1, Explanation = "Self-attention replaces recurrence in Transformers, allowing parallel processing and capturing long-range dependencies." },
            new() { Id = qId++, Text = "What are positional encodings for in Transformers?", Options = new() { "Normalize hidden states", "Add sequence order information since self-attention is position-agnostic", "Increase model capacity", "Apply dropout" }, CorrectIndex = 1, Explanation = "Positional encodings inject sequence position information since the self-attention mechanism itself has no notion of order." },
            new() { Id = qId++, Text = "Which Transformer variant excels at text generation tasks?", Options = new() { "Encoder-only (BERT)", "Encoder-decoder (T5)", "Decoder-only (GPT)", "All perform identically" }, CorrectIndex = 2, Explanation = "Decoder-only models like GPT are autoregressive generators, predicting one token at a time, making them ideal for generation." }
        }});

        // Lesson 91: Transfer Learning
        quizzes.Add(new Quiz { Id = qId++, LessonId = 91, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does fine-tuning a pretrained model involve?", Options = new() { "Training from scratch on a new dataset", "Continuing training on task-specific data to adapt pretrained weights", "Removing all pretrained layers", "Only changing the input layer" }, CorrectIndex = 1, Explanation = "Fine-tuning adapts a pretrained model by continuing training on task-specific data, often with a small learning rate." },
            new() { Id = qId++, Text = "When is feature extraction (frozen layers) preferred over full fine-tuning?", Options = new() { "When source and target tasks are very different", "When you have unlimited data", "When source and target tasks are similar and labeled data is limited", "When training from scratch is too fast" }, CorrectIndex = 2, Explanation = "Frozen feature extraction works well when domains are similar and task-specific data is limited, preventing overfitting." },
            new() { Id = qId++, Text = "Why did transfer learning revolutionize NLP?", Options = new() { "It eliminated the need for neural networks", "Large language models pretrained on text enable strong performance with minimal task-specific data", "It made models smaller", "It removed the need for tokenization" }, CorrectIndex = 1, Explanation = "Pretrained LLMs like BERT and GPT encode broad linguistic knowledge that can be fine-tuned efficiently for specific tasks." }
        }});

        // Lesson 92: AI Safety and Alignment
        quizzes.Add(new Quiz { Id = qId++, LessonId = 92, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is hallucination in AI language models?", Options = new() { "The model refusing to answer", "The model generating confident but factually incorrect outputs", "Slow model inference", "Excessive memory use" }, CorrectIndex = 1, Explanation = "Hallucination refers to AI models generating plausible-sounding but factually incorrect or fabricated information." },
            new() { Id = qId++, Text = "What does RLHF (Reinforcement Learning from Human Feedback) improve?", Options = new() { "GPU efficiency", "Training data volume", "Aligning model outputs with human preferences and values", "Model compression" }, CorrectIndex = 2, Explanation = "RLHF trains models to produce outputs that human raters prefer, improving alignment with human values and intentions." },
            new() { Id = qId++, Text = "What is the goal of AI interpretability research?", Options = new() { "Make models larger", "Speed up inference", "Understand what models learn internally to detect misalignment", "Reduce training costs" }, CorrectIndex = 2, Explanation = "Interpretability research aims to understand model internals, enabling detection of misaligned objectives and safer deployment." }
        }});

        // Lesson 93: Email Etiquette
        quizzes.Add(new Quiz { Id = qId++, LessonId = 93, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What makes an effective email subject line?", Options = new() { "Being short (one word)", "Being vague to create curiosity", "Being specific and informative about the content", "Including the recipient's name" }, CorrectIndex = 2, Explanation = "Specific subject lines help recipients prioritize, search for, and act on emails efficiently." },
            new() { Id = qId++, Text = "When should you use Reply All?", Options = new() { "Always", "Only when all recipients genuinely need your response", "Never in professional settings", "Only for announcements" }, CorrectIndex = 1, Explanation = "Reply All should only be used when your response is relevant and necessary for all recipients to see." },
            new() { Id = qId++, Text = "What does BCC protect when sending to multiple recipients?", Options = new() { "Message confidentiality", "Recipient privacy — BCC recipients cannot see each other's addresses", "Delivery confirmation", "Spam filtering" }, CorrectIndex = 1, Explanation = "BCC (Blind Carbon Copy) hides recipient addresses from each other, protecting privacy in group emails." }
        }});

        // Lesson 94: Giving and Receiving Feedback
        quizzes.Add(new Quiz { Id = qId++, LessonId = 94, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does the 'B' in the SBI feedback model stand for?", Options = new() { "Benefit", "Behavior", "Background", "Balance" }, CorrectIndex = 1, Explanation = "SBI stands for Situation, Behavior, Impact. Behavior describes the specific, observable action observed." },
            new() { Id = qId++, Text = "Why should feedback focus on behavior rather than personality?", Options = new() { "Personality is irrelevant", "Behavior is observable and changeable; personality judgments trigger defensiveness", "Personality cannot be discussed professionally", "Behavior feedback is faster to deliver" }, CorrectIndex = 1, Explanation = "Focusing on observable behavior keeps feedback objective and actionable, reducing defensiveness." },
            new() { Id = qId++, Text = "What is the recommended first response when receiving critical feedback?", Options = new() { "Immediately defend yourself", "Listen without interrupting and ask clarifying questions if needed", "Dismiss feedback you disagree with", "Agree with everything to avoid conflict" }, CorrectIndex = 1, Explanation = "Listening fully before responding allows you to understand the feedback completely before deciding how to act on it." }
        }});

        // Lesson 95: Conflict Resolution
        quizzes.Add(new Quiz { Id = qId++, LessonId = 95, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which conflict management style is both assertive and cooperative, seeking a solution that works for everyone?", Options = new() { "Competing", "Avoiding", "Collaborating", "Accommodating" }, CorrectIndex = 2, Explanation = "Collaborating is assertive and cooperative — it seeks solutions that fully satisfy both parties' interests." },
            new() { Id = qId++, Text = "Why is early conflict intervention usually better than waiting?", Options = new() { "It avoids documentation", "Small conflicts grow larger and more costly when left unaddressed", "Legal requirements mandate it", "It reduces workload immediately" }, CorrectIndex = 1, Explanation = "Unresolved conflict tends to escalate — unaddressed minor issues become major interpersonal breakdowns over time." },
            new() { Id = qId++, Text = "What role does a mediator play in conflict resolution?", Options = new() { "Decide who is right", "Take one side in the dispute", "Act as a neutral facilitator to help parties reach their own resolution", "Enforce company policy" }, CorrectIndex = 2, Explanation = "A mediator is a neutral third party who facilitates structured dialogue to help parties resolve conflict themselves." }
        }});

        // Lesson 96: Cross-Cultural Communication
        quizzes.Add(new Quiz { Id = qId++, LessonId = 96, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What characterizes high-context cultures?", Options = new() { "Very explicit, direct communication", "Relying on implicit meaning, relationships, and nonverbal cues", "Preference for written over spoken communication", "Strong preference for formal titles always" }, CorrectIndex = 1, Explanation = "High-context cultures (e.g., Japan, many Arab cultures) rely on shared context and indirect communication." },
            new() { Id = qId++, Text = "Why should idioms and slang be avoided in cross-cultural communication?", Options = new() { "They are unprofessional", "They may not translate and cause confusion or misunderstanding", "They are too informal for business", "They slow down conversation" }, CorrectIndex = 1, Explanation = "Idioms and slang often don't translate across cultures, leading to confusion or misinterpretation of meaning." },
            new() { Id = qId++, Text = "What is Cultural Intelligence (CQ)?", Options = new() { "IQ measured across cultures", "The ability to work and communicate effectively across cultural boundaries", "Knowledge of multiple languages only", "Understanding of cultural history" }, CorrectIndex = 1, Explanation = "Cultural Intelligence (CQ) encompasses awareness of your own cultural assumptions, knowledge of others, and ability to adapt." }
        }});

        // Lesson 97: Negotiation Skills
        quizzes.Add(new Quiz { Id = qId++, LessonId = 97, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is BATNA?", Options = new() { "A bargaining tactic", "Best Alternative to a Negotiated Agreement — your fallback if negotiation fails", "A type of contract", "A mediation technique" }, CorrectIndex = 1, Explanation = "BATNA defines what you will do if no agreement is reached — knowing it strengthens your negotiating position." },
            new() { Id = qId++, Text = "What does interest-based negotiation focus on instead of positions?", Options = new() { "Legal requirements", "Personal relationships only", "Underlying needs and motivations driving each party's stated position", "Financial metrics only" }, CorrectIndex = 2, Explanation = "Interest-based negotiation explores what parties actually need rather than just their stated demands, opening creative solutions." },
            new() { Id = qId++, Text = "Why is anchoring important in negotiation?", Options = new() { "It ends the negotiation quickly", "The first offer sets the psychological reference point that shapes subsequent discussion", "It guarantees a better outcome", "It eliminates the need for compromise" }, CorrectIndex = 1, Explanation = "The first number introduced (the anchor) has a strong influence on the range within which agreement is reached." }
        }});

        // Lesson 98: Building High-Performing Teams
        quizzes.Add(new Quiz { Id = qId++, LessonId = 98, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "According to Google's Project Aristotle, what is the #1 predictor of team effectiveness?", Options = new() { "Team size", "Individual IQ scores", "Psychological safety", "Number of senior members" }, CorrectIndex = 2, Explanation = "Psychological safety — feeling safe to take risks and speak up — was found to be the strongest predictor of team performance." },
            new() { Id = qId++, Text = "In Tuckman's team development model, what happens during the Storming stage?", Options = new() { "The team reaches peak performance", "Conflict emerges as team members assert their ideas and styles", "The team disbands", "Roles are informally cemented" }, CorrectIndex = 1, Explanation = "Storming is characterized by conflict and tension as team members navigate differences in working styles and ideas." },
            new() { Id = qId++, Text = "How does a leader best enable diversity of thought to improve outcomes?", Options = new() { "Assign tasks without discussion", "Create psychological safety so all voices are genuinely heard", "Only include senior team members in decisions", "Limit meeting time to reduce conflict" }, CorrectIndex = 1, Explanation = "Without psychological safety, diverse perspectives remain unexpressed. Safety enables the full value of diversity to be realized." }
        }});

        // Lesson 99: Delegation and Empowerment
        quizzes.Add(new Quiz { Id = qId++, LessonId = 99, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the most common reason leaders fail to delegate?", Options = new() { "Too many meetings", "Perfectionism or belief it is faster to do it themselves", "Lack of team members", "No time to assign work" }, CorrectIndex = 1, Explanation = "Perfectionism and the perception that 'doing it myself is faster' are the most common barriers to effective delegation." },
            new() { Id = qId++, Text = "When delegating, what should be communicated clearly?", Options = new() { "The exact method the person must use", "Only the deadline", "The goal and success criteria, with freedom on the method", "Budget details only" }, CorrectIndex = 2, Explanation = "Effective delegation defines what success looks like and by when, while leaving how to achieve it to the person." },
            new() { Id = qId++, Text = "What distinguishes empowerment from simple task delegation?", Options = new() { "No difference", "Empowerment gives genuine decision-making authority within defined boundaries", "Empowerment means no oversight at all", "Delegation provides more responsibility" }, CorrectIndex = 1, Explanation = "Empowerment gives people real authority over decisions, not just tasks — creating genuine ownership and accountability." }
        }});

        // Lesson 100: Performance Management
        quizzes.Add(new Quiz { Id = qId++, LessonId = 100, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does the 'M' in SMART goals stand for?", Options = new() { "Motivating", "Manageable", "Measurable", "Meaningful" }, CorrectIndex = 2, Explanation = "SMART goals are Specific, Measurable, Achievable, Relevant, and Time-bound. Measurable means progress can be tracked objectively." },
            new() { Id = qId++, Text = "A good Performance Improvement Plan (PIP) should primarily focus on:", Options = new() { "Documenting the case for dismissal", "Defining the performance gap and providing structured support for improvement", "Assigning more tasks", "Peer comparisons" }, CorrectIndex = 1, Explanation = "A well-designed PIP should be improvement-focused, clearly defining the gap, expected outcomes, and support provided." },
            new() { Id = qId++, Text = "Why should the annual performance review hold no surprises?", Options = new() { "Reviews are just formalities", "Ongoing feedback throughout the year should have already addressed key points", "Surprises motivate employees", "HR requires no new information" }, CorrectIndex = 1, Explanation = "Regular feedback throughout the year means the formal review documents already-discussed points rather than introducing new revelations." }
        }});

        // Lesson 101: Change Management
        quizzes.Add(new Quiz { Id = qId++, LessonId = 101, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is the first step in Kotter's 8-Step Change Model?", Options = new() { "Build a guiding coalition", "Create urgency for the change", "Institute the change in culture", "Generate short-term wins" }, CorrectIndex = 1, Explanation = "Kotter's model starts with creating urgency — helping people understand why change is necessary and needed now." },
            new() { Id = qId++, Text = "What does the ADKAR model focus on?", Options = new() { "Organizational structure changes", "The individual human transition through change", "IT system migration steps", "Financial planning for change" }, CorrectIndex = 1, Explanation = "ADKAR (Awareness, Desire, Knowledge, Ability, Reinforcement) focuses on the individual's journey through change." },
            new() { Id = qId++, Text = "Why do people most commonly resist organizational change?", Options = new() { "They are lazy", "Fear of the unknown, loss of control, and lack of trust in leadership", "They prefer more work", "Change is always objectively harmful" }, CorrectIndex = 1, Explanation = "Resistance typically reflects understandable concerns: uncertainty, workload, skill gaps, and past negative experiences." }
        }});

        // Lesson 102: Strategic Thinking
        quizzes.Add(new Quiz { Id = qId++, LessonId = 102, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What does SWOT analysis assess?", Options = new() { "System, Workflow, Operations, Technology", "Strengths, Weaknesses, Opportunities, Threats", "Strategy, Win, Objectives, Tactics", "Software, Web, Output, Testing" }, CorrectIndex = 1, Explanation = "SWOT analysis evaluates internal Strengths and Weaknesses alongside external Opportunities and Threats." },
            new() { Id = qId++, Text = "What do OKRs help achieve in an organization?", Options = new() { "Employee compensation planning", "Aligning team and individual goals with organizational strategy through objectives and measurable key results", "Resource allocation only", "Project timeline management" }, CorrectIndex = 1, Explanation = "OKRs (Objectives and Key Results) create alignment between individual/team work and organizational strategy." },
            new() { Id = qId++, Text = "What distinguishes strategic thinking from tactical execution?", Options = new() { "Strategic thinking focuses on immediate tasks", "Strategic thinking asks why and sees the big picture; tactical execution focuses on how", "They are the same thing", "Tactics are more important than strategy" }, CorrectIndex = 1, Explanation = "Strategic thinking looks at the big picture, long-term direction, and underlying 'why' — beyond the immediate 'how.'" }
        }});

        // Lesson 103: Peripherals and Connectivity
        quizzes.Add(new Quiz { Id = qId++, LessonId = 103, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What data transfer speed does USB 3.2 Gen 2 support?", Options = new() { "480 Mbps", "5 Gbps", "10 Gbps", "40 Gbps" }, CorrectIndex = 2, Explanation = "USB 3.2 Gen 2 supports 10 Gbps data transfer speeds over USB-A or USB-C connectors." },
            new() { Id = qId++, Text = "What can a single Thunderbolt 4 port carry?", Options = new() { "Data only", "Video only", "Data, video (DisplayPort), and Power Delivery over one cable", "Audio only" }, CorrectIndex = 2, Explanation = "Thunderbolt 4 over USB-C can simultaneously carry data (40 Gbps), DisplayPort video, and up to 100W Power Delivery." },
            new() { Id = qId++, Text = "Why might a competitive gamer prefer a wired mouse over Bluetooth?", Options = new() { "Wired mice are lighter", "Wired mice have lower and more consistent latency", "Bluetooth mice have shorter battery life", "Wired mice cost less" }, CorrectIndex = 1, Explanation = "Wired peripherals have lower, more consistent latency than Bluetooth, which matters for precision and competitive gaming." }
        }});

        // Lesson 104: PC Building Process
        quizzes.Add(new Quiz { Id = qId++, LessonId = 104, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Why is thermal paste applied between the CPU and cooler?", Options = new() { "To glue the cooler in place", "To fill microscopic gaps and improve heat transfer from CPU to cooler", "To prevent electrical shorts", "To increase CPU clock speed" }, CorrectIndex = 1, Explanation = "Thermal paste fills microscopic imperfections between the CPU and cooler surfaces, maximizing thermal conductivity." },
            new() { Id = qId++, Text = "What is the first step in the PC assembly sequence?", Options = new() { "Install the GPU", "Mount the PSU", "Install the CPU into the motherboard socket", "Connect power cables" }, CorrectIndex = 2, Explanation = "Installing the CPU first (before mounting the motherboard in the case) is easier when the board is flat on an anti-static surface." },
            new() { Id = qId++, Text = "Why should you perform a bench test before closing the case?", Options = new() { "Cases slow down booting", "It allows quick reseating of components if any issues arise before final assembly", "Cases trap heat during POST", "Motherboards require air exposure during first boot" }, CorrectIndex = 1, Explanation = "Testing outside the case makes it easy to reseat components and diagnose issues before final installation." }
        }});

        // Lesson 105: Overclocking and Tuning
        quizzes.Add(new Quiz { Id = qId++, LessonId = 105, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What type of CPU supports overclocking on Intel platforms?", Options = new() { "Any Intel CPU", "Only server CPUs", "Intel 'K' suffix CPUs with unlocked multipliers", "Budget i3 CPUs" }, CorrectIndex = 2, Explanation = "Intel 'K' series CPUs (e.g., Core i7-14700K) have unlocked multipliers that allow overclocking in BIOS." },
            new() { Id = qId++, Text = "What does enabling XMP/EXPO in BIOS do for RAM?", Options = new() { "Increases voltage to dangerous levels", "Activates the memory kit's rated speed profile automatically", "Enables ECC error correction", "Doubles the number of memory channels" }, CorrectIndex = 1, Explanation = "XMP (Intel) and EXPO (AMD) profiles apply validated speed settings for RAM kits that exceed default JEDEC speeds." },
            new() { Id = qId++, Text = "Why is stability testing essential after overclocking a CPU?", Options = new() { "To measure power consumption only", "To verify the overclock is stable under sustained load and won't cause crashes", "To update BIOS firmware", "To set fan curves" }, CorrectIndex = 1, Explanation = "Stability testing under load (Prime95, Cinebench) confirms the overclock won't cause crashes or data corruption in real workloads." }
        }});

        // Lesson 106: PC Troubleshooting
        quizzes.Add(new Quiz { Id = qId++, LessonId = 106, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What do POST beep codes indicate?", Options = new() { "Successful boot completion", "Specific hardware failures identified during Power-On Self-Test", "Fan speed errors", "BIOS update availability" }, CorrectIndex = 1, Explanation = "POST beep codes indicate specific hardware failures detected during the power-on self-test before the OS loads." },
            new() { Id = qId++, Text = "Which tool checks the health of an SSD or HDD?", Options = new() { "MemTest86", "CrystalDiskInfo", "CPU-Z", "GPU-Z" }, CorrectIndex = 1, Explanation = "CrystalDiskInfo reads S.M.A.R.T. data from drives to assess their health and predict potential failure." },
            new() { Id = qId++, Text = "If a PC boots normally in Safe Mode but crashes normally, what is likely the cause?", Options = new() { "Faulty RAM", "A driver or startup program conflict, since Safe Mode loads minimal drivers", "Overheating CPU", "Bad power supply" }, CorrectIndex = 1, Explanation = "Safe Mode loads only essential drivers — if problems disappear, a third-party driver or startup program is likely the culprit." }
        }});

        // Lesson 107: Laptop vs Desktop
        quizzes.Add(new Quiz { Id = qId++, LessonId = 107, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What is a key advantage of desktops over laptops for high-performance workloads?", Options = new() { "Built-in battery", "Better portability", "Larger thermal headroom allowing sustained higher performance", "Lower cost always" }, CorrectIndex = 2, Explanation = "Desktop cases allow larger cooling solutions, enabling sustained high-performance workloads without thermal throttling." },
            new() { Id = qId++, Text = "What makes modern laptops difficult to upgrade?", Options = new() { "Complex operating systems", "RAM and storage are often soldered directly to the motherboard", "Proprietary software restrictions", "Incompatible drivers" }, CorrectIndex = 1, Explanation = "Many modern laptops solder RAM and SSDs onto the board, making upgrades impossible or very difficult." },
            new() { Id = qId++, Text = "What setup gives a mobile professional the best of both laptop and desktop?", Options = new() { "Carrying a desktop to meetings", "Two separate computers", "A laptop with a docking station and external monitor at a fixed desk", "Only using cloud computing" }, CorrectIndex = 2, Explanation = "A laptop with a docking station provides a full desktop experience at a fixed desk while maintaining full portability." }
        }});

        // Lesson 108: Cloud Storage Services
        quizzes.Add(new Quiz { Id = qId++, LessonId = 108, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What durability guarantee does Amazon S3 provide for stored objects?", Options = new() { "99%", "99.9%", "99.99%", "99.999999999% (11 nines)" }, CorrectIndex = 3, Explanation = "Amazon S3 provides 99.999999999% (11 nines) durability through redundant replication across multiple locations." },
            new() { Id = qId++, Text = "What does client-side encryption ensure when using cloud storage?", Options = new() { "Faster uploads", "Only the user can read the data, even if the provider is compromised", "Free storage", "Automatic version control" }, CorrectIndex = 1, Explanation = "Client-side encryption ensures files are encrypted before upload, so only the key holder (you) can decrypt the data." },
            new() { Id = qId++, Text = "What is a significant limitation of cloud storage?", Options = new() { "Cannot store large files", "Requires constant internet access and introduces ongoing subscription costs", "Only works on Windows", "No sharing features" }, CorrectIndex = 1, Explanation = "Cloud storage requires internet connectivity and ongoing subscription fees, unlike one-time hardware purchases." }
        }});

        // Lesson 109: Network Attached Storage
        quizzes.Add(new Quiz { Id = qId++, LessonId = 109, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What protocol does NAS use to share files with Windows computers?", Options = new() { "FTP", "iSCSI", "SMB/CIFS", "HTTP" }, CorrectIndex = 2, Explanation = "SMB/CIFS (Server Message Block) is the standard Windows file sharing protocol, supported by all major NAS devices." },
            new() { Id = qId++, Text = "What is a key advantage of NAS over cloud storage?", Options = new() { "Available without local network", "No power needed", "Faster local access speeds and full data sovereignty", "Managed by a third party" }, CorrectIndex = 2, Explanation = "NAS provides gigabit local access speeds and you own all data, unlike cloud storage managed by a third-party provider." },
            new() { Id = qId++, Text = "What popular media server software runs on NAS to stream your collection?", Options = new() { "Wireshark", "Plex", "FileZilla", "Nagios" }, CorrectIndex = 1, Explanation = "Plex Media Server runs on NAS devices to stream video, music, and photos to any device on your network or remotely." }
        }});

        // Lesson 110: Storage Area Networks
        quizzes.Add(new Quiz { Id = qId++, LessonId = 110, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "What level of storage access does a SAN provide?", Options = new() { "File-level (like NAS)", "Block-level — raw storage that servers manage with their own file system", "Object-level", "Message-level" }, CorrectIndex = 1, Explanation = "SANs provide block-level access, allowing servers to treat SAN volumes as if they were locally attached drives." },
            new() { Id = qId++, Text = "Which SAN technology uses standard Ethernet networks to transport storage commands?", Options = new() { "Fibre Channel (FC)", "iSCSI", "NVMe-oF over InfiniBand", "USB 4" }, CorrectIndex = 1, Explanation = "iSCSI transports SCSI storage commands over standard IP/Ethernet networks, reducing cost compared to dedicated Fibre Channel." },
            new() { Id = qId++, Text = "What does thin provisioning allow in a SAN environment?", Options = new() { "Physical space is allocated only as data is actually written, not upfront", "Data is automatically compressed", "Volumes are mirrored automatically", "Servers share CPU resources" }, CorrectIndex = 0, Explanation = "Thin provisioning presents larger logical volumes to servers but only consumes physical space as data is actually written." }
        }});

        // Lesson 111: Data Deduplication and Compression
        quizzes.Add(new Quiz { Id = qId++, LessonId = 111, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "Which type of compression is suitable for executable files and databases?", Options = new() { "Lossy compression", "Lossless compression", "Perceptual compression", "Delta compression only" }, CorrectIndex = 1, Explanation = "Lossless compression preserves data exactly and is required for executables, code, and databases where every bit matters." },
            new() { Id = qId++, Text = "Why does deduplication achieve high ratios on VM backup sets?", Options = new() { "VMs compress very well individually", "Many blocks are identical across multiple VM snapshots and copies", "VMs have no unique data", "Deduplication only works on databases" }, CorrectIndex = 1, Explanation = "VM images share many identical OS and application blocks across snapshots and clones, allowing deduplication ratios of 10:1 or more." },
            new() { Id = qId++, Text = "What does inline deduplication do compared to post-process deduplication?", Options = new() { "Processes data after it is stored", "Processes data before writing, saving space immediately but requiring more CPU", "Only works on SSDs", "Requires manual triggering" }, CorrectIndex = 1, Explanation = "Inline deduplication processes data as it arrives, saving space immediately at the cost of higher real-time CPU overhead." }
        }});

        // Lesson 112: Object Storage and Modern Architectures
        quizzes.Add(new Quiz { Id = qId++, LessonId = 112, Questions = new List<Question>
        {
            new() { Id = qId++, Text = "How is data accessed in object storage?", Options = new() { "By file path in a directory tree", "By block address on a disk", "Via HTTP API using a unique object key", "Via SQL queries" }, CorrectIndex = 2, Explanation = "Object storage uses HTTP APIs (GET, PUT, DELETE) with unique object keys in flat bucket namespaces." },
            new() { Id = qId++, Text = "What is a key benefit of object storage for large-scale data?", Options = new() { "Low-latency random I/O", "Virtually unlimited scalability and high durability through geographic replication", "Better performance for databases than block storage", "Free to use always" }, CorrectIndex = 1, Explanation = "Object storage scales to exabytes and achieves extreme durability through redundant replication across locations." },
            new() { Id = qId++, Text = "For which workload is object storage NOT recommended?", Options = new() { "Storing images and videos", "ML training datasets", "Relational database files requiring low-latency random read/write", "Log file archiving" }, CorrectIndex = 2, Explanation = "Object storage has high latency for random access — databases requiring fast random I/O should use block storage instead." }
        }});

        return quizzes;
    }

    private static List<MiniGame> BuildMiniGames()
    {
        var games = new List<MiniGame>();
        int gId = 1;

        games.Add(new MiniGame { Id = gId++, LessonId = 1, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "CPU", Definition = "Central Processing Unit — the brain of the computer that executes instructions" },
            new() { Term = "RAM", Definition = "Random Access Memory — volatile short-term storage used by the CPU" },
            new() { Term = "HDD", Definition = "Hard Disk Drive — magnetic storage that persists data without power" },
            new() { Term = "Input Device", Definition = "Hardware used to send data to the computer (keyboard, mouse)" },
            new() { Term = "Output Device", Definition = "Hardware that displays or transmits data from the computer (monitor, speakers)" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 2, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Kernel", Definition = "The core of the OS that directly manages hardware resources" },
            new() { Term = "Multitasking", Definition = "Running multiple programs simultaneously by the OS" },
            new() { Term = "Virtual Memory", Definition = "Using disk space as additional RAM when physical RAM is full" },
            new() { Term = "Driver", Definition = "Software that allows the OS to communicate with hardware devices" },
            new() { Term = "Shell", Definition = "The user-facing interface of the OS (command line or GUI)" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 3, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "CIA Triad", Definition = "Confidentiality, Integrity, Availability — the three pillars of information security" },
            new() { Term = "Malware", Definition = "Malicious software designed to damage or gain unauthorized access to systems" },
            new() { Term = "Phishing", Definition = "A social engineering attack that tricks users into revealing credentials" },
            new() { Term = "Firewall", Definition = "A security system that monitors and controls incoming and outgoing network traffic" },
            new() { Term = "DDoS", Definition = "Distributed Denial of Service — overwhelming a server to take it offline" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 4, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Encryption", Definition = "Transforming readable data into an unreadable format using an algorithm and key" },
            new() { Term = "AES", Definition = "Advanced Encryption Standard — a widely used symmetric encryption algorithm" },
            new() { Term = "RSA", Definition = "Asymmetric encryption algorithm used in HTTPS and digital signatures" },
            new() { Term = "2FA", Definition = "Two-Factor Authentication — a second verification step beyond a password" },
            new() { Term = "Plaintext", Definition = "Readable, unencrypted data before encryption is applied" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 5, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Variable", Definition = "A named container that stores a value in memory" },
            new() { Term = "int", Definition = "Integer data type — whole numbers like 42 or -7" },
            new() { Term = "str", Definition = "String data type — text values like 'Hello World'" },
            new() { Term = "bool", Definition = "Boolean data type — only True or False values" },
            new() { Term = "dict", Definition = "Dictionary — a collection of key-value pairs" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 6, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "if statement", Definition = "Executes a block of code only if a condition is True" },
            new() { Term = "for loop", Definition = "Iterates over each item in a sequence" },
            new() { Term = "while loop", Definition = "Repeats a block as long as a condition remains True" },
            new() { Term = "break", Definition = "Immediately exits the current loop" },
            new() { Term = "list comprehension", Definition = "Concise syntax to create a list: [expr for item in iterable]" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 7, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "HTML", Definition = "HyperText Markup Language — the structure of web pages" },
            new() { Term = "<a> tag", Definition = "Anchor tag used to create hyperlinks with an href attribute" },
            new() { Term = "<div>", Definition = "A block-level container element for grouping HTML content" },
            new() { Term = "Semantic HTML", Definition = "Tags that describe the meaning of content (header, article, nav)" },
            new() { Term = "alt attribute", Definition = "Provides alternative text for images, important for accessibility" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 8, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "CSS", Definition = "Cascading Style Sheets — controls the visual presentation of HTML" },
            new() { Term = "Box Model", Definition = "Content, Padding, Border, Margin — how every HTML element occupies space" },
            new() { Term = "Flexbox", Definition = "A CSS layout model for arranging items in one dimension (row or column)" },
            new() { Term = "Media Query", Definition = "@media rule that applies styles based on screen size for responsive design" },
            new() { Term = "Specificity", Definition = "The rule that determines which CSS style wins when multiple rules apply" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 9, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Simple Present", Definition = "Used for facts and habits: 'She walks to school.'" },
            new() { Term = "Present Continuous", Definition = "Action happening now: 'She is walking to school.'" },
            new() { Term = "Present Perfect", Definition = "Past action with present relevance: 'She has walked to school.'" },
            new() { Term = "Simple Past", Definition = "Completed past action: 'She walked to school yesterday.'" },
            new() { Term = "Simple Future", Definition = "Future action: 'She will walk to school tomorrow.'" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 10, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Noun", Definition = "A word that names a person, place, thing, or idea" },
            new() { Term = "Verb", Definition = "A word that expresses an action or state of being" },
            new() { Term = "Adjective", Definition = "A word that describes or modifies a noun" },
            new() { Term = "Adverb", Definition = "A word that modifies a verb, adjective, or another adverb" },
            new() { Term = "Conjunction", Definition = "A word that connects words, phrases, or clauses (and, but, or)" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 11, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Hola", Definition = "Hello" },
            new() { Term = "Buenos días", Definition = "Good morning" },
            new() { Term = "¿Cómo te llamas?", Definition = "What is your name? (informal)" },
            new() { Term = "Mucho gusto", Definition = "Nice to meet you" },
            new() { Term = "Usted", Definition = "Formal form of 'you' in Spanish" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 12, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Uno, Dos, Tres", Definition = "One, Two, Three" },
            new() { Term = "Rojo", Definition = "Red" },
            new() { Term = "Azul", Definition = "Blue" },
            new() { Term = "Verde", Definition = "Green" },
            new() { Term = "Amarillo", Definition = "Yellow" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 13, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "LAN", Definition = "Local Area Network — a network covering a small area like a home or office" },
            new() { Term = "Router", Definition = "A device that connects different networks and routes traffic between them" },
            new() { Term = "Switch", Definition = "A device that connects multiple devices within the same network" },
            new() { Term = "WAN", Definition = "Wide Area Network — a network spanning large geographic areas" },
            new() { Term = "Client-Server", Definition = "Network model where servers provide services and clients request them" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 14, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "IPv4", Definition = "32-bit IP addressing with ~4.3 billion unique addresses" },
            new() { Term = "IPv6", Definition = "128-bit IP addressing in hexadecimal, solving IPv4 address exhaustion" },
            new() { Term = "Subnet Mask", Definition = "Divides IP addresses into network and host portions" },
            new() { Term = "NAT", Definition = "Network Address Translation — multiple devices share one public IP" },
            new() { Term = "Private IP", Definition = "IP ranges not routable on internet: 10.x, 172.16-31.x, 192.168.x" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 15, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Layer 7 - Application", Definition = "User-facing protocols: HTTP, FTP, DNS, SMTP" },
            new() { Term = "Layer 4 - Transport", Definition = "Reliable data transfer: TCP and UDP" },
            new() { Term = "Layer 3 - Network", Definition = "Logical addressing and routing: IP protocol" },
            new() { Term = "Layer 2 - Data Link", Definition = "Physical addressing (MAC) and error detection: Ethernet" },
            new() { Term = "Layer 1 - Physical", Definition = "Bits transmitted over cables, fiber, or wireless signals" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 16, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "TCP", Definition = "Transmission Control Protocol — connection-oriented, guaranteed delivery" },
            new() { Term = "UDP", Definition = "User Datagram Protocol — connectionless, fast, no delivery guarantee" },
            new() { Term = "Three-Way Handshake", Definition = "TCP connection setup: SYN → SYN-ACK → ACK" },
            new() { Term = "Port 443", Definition = "Standard port for HTTPS (encrypted web traffic)" },
            new() { Term = "Socket", Definition = "A combination of IP address and port number (e.g., 192.168.1.1:443)" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 17, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Supervised Learning", Definition = "ML with labeled training data — learns input-output mappings" },
            new() { Term = "Unsupervised Learning", Definition = "ML finding patterns in unlabeled data (clustering, dimensionality reduction)" },
            new() { Term = "Reinforcement Learning", Definition = "Agent learns by interacting with environment and receiving rewards/penalties" },
            new() { Term = "Training Data", Definition = "Labeled examples used to train an ML model" },
            new() { Term = "scikit-learn", Definition = "Popular Python library for machine learning algorithms" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 18, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Classification", Definition = "Predicting a category (spam/not spam, cat/dog)" },
            new() { Term = "Regression", Definition = "Predicting a continuous value (house price, temperature)" },
            new() { Term = "Overfitting", Definition = "Model memorizes training data but fails to generalize to new data" },
            new() { Term = "Cross-Validation", Definition = "Technique to assess model generalizability using different data subsets" },
            new() { Term = "Random Forest", Definition = "Ensemble ML algorithm using many decision trees for better accuracy" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 19, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Neuron", Definition = "A node in a neural network that receives inputs, applies weights, and outputs a value" },
            new() { Term = "Activation Function", Definition = "Introduces non-linearity in neurons (ReLU, Sigmoid, Tanh)" },
            new() { Term = "Backpropagation", Definition = "Algorithm that propagates error backward to update network weights" },
            new() { Term = "Epoch", Definition = "One complete pass through the entire training dataset" },
            new() { Term = "Loss Function", Definition = "Measures the difference between predicted and actual output" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 20, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "CNN", Definition = "Convolutional Neural Network — specialized for image processing tasks" },
            new() { Term = "RNN", Definition = "Recurrent Neural Network — processes sequential data (text, time series)" },
            new() { Term = "Transformer", Definition = "Architecture using self-attention, powering GPT and BERT models" },
            new() { Term = "GAN", Definition = "Generative Adversarial Network — generates realistic synthetic data" },
            new() { Term = "LLM", Definition = "Large Language Model — AI trained on vast text to understand and generate language" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 21, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Active Listening", Definition = "Fully concentrating on and understanding a speaker's message" },
            new() { Term = "Non-verbal Communication", Definition = "Body language, facial expressions, gestures, and tone of voice" },
            new() { Term = "Feedback", Definition = "Response from receiver confirming a message was understood" },
            new() { Term = "Communication Barrier", Definition = "Anything that distorts or blocks effective communication" },
            new() { Term = "Paraphrasing", Definition = "Restating someone's message in your own words to confirm understanding" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 22, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Hook", Definition = "An engaging opening for a speech (story, question, surprising fact)" },
            new() { Term = "Eye Contact", Definition = "Looking at different audience members to build connection and trust" },
            new() { Term = "Pace", Definition = "Speed of speaking — moderate pace aids clarity and comprehension" },
            new() { Term = "Pause", Definition = "A deliberate silence used for emphasis or to let a point sink in" },
            new() { Term = "Call to Action", Definition = "A clear request for the audience to do something after your speech" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 23, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Autocratic Leadership", Definition = "Leader makes decisions unilaterally with little team input" },
            new() { Term = "Democratic Leadership", Definition = "Leader involves team in decision-making processes" },
            new() { Term = "Transformational Leadership", Definition = "Leader inspires through compelling vision and personal example" },
            new() { Term = "Servant Leadership", Definition = "Leader prioritizes team's needs and development above personal gain" },
            new() { Term = "Situational Leadership", Definition = "Adapting leadership style to each team member's competence and commitment" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 24, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Self-awareness", Definition = "Knowing and understanding your own emotions and triggers" },
            new() { Term = "Self-regulation", Definition = "Managing and controlling your own emotional responses" },
            new() { Term = "Empathy", Definition = "Understanding and sharing the feelings of others" },
            new() { Term = "Social Skills", Definition = "Ability to manage relationships and communicate effectively" },
            new() { Term = "EQ", Definition = "Emotional Quotient — measure of emotional intelligence" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 25, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "CPU", Definition = "Central Processing Unit — executes instructions and performs calculations" },
            new() { Term = "Clock Speed", Definition = "How many cycles a CPU executes per second, measured in GHz" },
            new() { Term = "CPU Cache", Definition = "Ultra-fast memory (L1/L2/L3) built near the processor to reduce latency" },
            new() { Term = "Motherboard", Definition = "Main circuit board connecting all computer components" },
            new() { Term = "UEFI", Definition = "Modern replacement for BIOS firmware for system initialization" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 26, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "RAM", Definition = "Volatile short-term memory storing data the CPU is actively using" },
            new() { Term = "DDR5", Definition = "Latest RAM standard offering faster speeds than DDR4" },
            new() { Term = "GPU", Definition = "Graphics Processing Unit — handles graphics and parallel computing tasks" },
            new() { Term = "VRAM", Definition = "Video RAM on a GPU for storing textures and frame buffers" },
            new() { Term = "Integrated Graphics", Definition = "GPU built into the CPU or motherboard, sharing system RAM" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 27, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "HDD", Definition = "Hard Disk Drive — stores data on spinning magnetic platters" },
            new() { Term = "SSD", Definition = "Solid State Drive — stores data in flash memory chips, much faster than HDD" },
            new() { Term = "NVMe", Definition = "Non-Volatile Memory Express — high-speed SSD protocol using PCIe" },
            new() { Term = "SATA", Definition = "Serial ATA interface used by HDDs and some SSDs (~550 MB/s)" },
            new() { Term = "TRIM", Definition = "SSD feature that maintains performance by managing deleted data blocks" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 28, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "RAID 0", Definition = "Striping — maximum performance, no redundancy" },
            new() { Term = "RAID 1", Definition = "Mirroring — data duplicated across two drives for redundancy" },
            new() { Term = "RAID 5", Definition = "Striping with parity across 3+ drives, tolerates one failure" },
            new() { Term = "RAID 10", Definition = "Combines mirroring and striping — fast and redundant, needs 4+ drives" },
            new() { Term = "3-2-1 Rule", Definition = "3 copies, 2 media types, 1 offsite backup for disaster recovery" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 29, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Processing", Definition = "Execution of instructions mainly by the CPU" },
            new() { Term = "Memory", Definition = "Fast temporary storage, primarily RAM" },
            new() { Term = "Storage", Definition = "Persistent data retention via SSDs or HDDs" },
            new() { Term = "Bottleneck", Definition = "A component limiting overall system performance" },
            new() { Term = "Thermal Throttling", Definition = "Automatic speed reduction to prevent overheating" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 30, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Zero Trust", Definition = "Security model where no user or device is trusted by default" },
            new() { Term = "Segmentation", Definition = "Splitting networks into isolated zones for security" },
            new() { Term = "MFA", Definition = "Multi-factor authentication requiring more than one verification factor" },
            new() { Term = "IDS", Definition = "Intrusion Detection System monitoring suspicious network behavior" },
            new() { Term = "VPN", Definition = "Encrypted tunnel for secure remote network access" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 31, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "def", Definition = "Python keyword used to define a function" },
            new() { Term = "Parameter", Definition = "Input accepted by a function" },
            new() { Term = "Return Value", Definition = "Output produced by a function" },
            new() { Term = "Module", Definition = "Python file containing reusable code" },
            new() { Term = "import", Definition = "Statement used to load code from another module" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 32, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "DOM", Definition = "Document Object Model representing page structure" },
            new() { Term = "Event", Definition = "User or browser action JavaScript can respond to" },
            new() { Term = "Promise", Definition = "Object representing future completion of async work" },
            new() { Term = "async/await", Definition = "Syntax for writing asynchronous code clearly" },
            new() { Term = "Module", Definition = "Reusable JavaScript file imported with export/import" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 33, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Subject", Definition = "Who or what performs the action in a sentence" },
            new() { Term = "Verb", Definition = "Word expressing action or state" },
            new() { Term = "Object", Definition = "Receiver of the action in a sentence" },
            new() { Term = "Compound Sentence", Definition = "Sentence with two independent clauses" },
            new() { Term = "Transition", Definition = "Word/phrase connecting ideas smoothly" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 34, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Ser", Definition = "To be (identity, characteristics)" },
            new() { Term = "Estar", Definition = "To be (state, location)" },
            new() { Term = "Tener", Definition = "To have" },
            new() { Term = "Ir", Definition = "To go" },
            new() { Term = "Hacer", Definition = "To do or make" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 35, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Star Topology", Definition = "Devices connect through a central switch or hub" },
            new() { Term = "Bus Topology", Definition = "All devices share one main communication line" },
            new() { Term = "Ring Topology", Definition = "Devices form a closed loop data path" },
            new() { Term = "Mesh Topology", Definition = "Multiple paths between nodes for high redundancy" },
            new() { Term = "Logical Topology", Definition = "How data flows regardless of physical cabling" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 36, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "HTTP", Definition = "Protocol for transferring web resources" },
            new() { Term = "HTTPS", Definition = "HTTP secured with TLS encryption" },
            new() { Term = "DNS", Definition = "System translating domain names to IP addresses" },
            new() { Term = "SMTP", Definition = "Protocol used to send email" },
            new() { Term = "FTP/SFTP", Definition = "Protocols for transferring files across networks" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 37, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Precision", Definition = "How many predicted positives are truly positive" },
            new() { Term = "Recall", Definition = "How many actual positives were correctly found" },
            new() { Term = "F1-Score", Definition = "Balance metric combining precision and recall" },
            new() { Term = "MAE", Definition = "Mean Absolute Error for regression tasks" },
            new() { Term = "Confusion Matrix", Definition = "Table showing classification prediction outcomes" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 38, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Learning Rate", Definition = "Controls update step size during training" },
            new() { Term = "Batch Size", Definition = "Number of samples used in one training update" },
            new() { Term = "Dropout", Definition = "Regularization method that randomly disables neurons" },
            new() { Term = "Early Stopping", Definition = "Stops training when validation stops improving" },
            new() { Term = "Weight Decay", Definition = "Regularization penalizing large model weights" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 39, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Context", Definition = "Background information needed to understand a message" },
            new() { Term = "Call to Action", Definition = "Specific request for what the reader should do" },
            new() { Term = "Concise Writing", Definition = "Using clear, direct language without unnecessary detail" },
            new() { Term = "Audience", Definition = "People the message is intended for" },
            new() { Term = "Ambiguity", Definition = "Unclear wording that can cause misunderstandings" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 40, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Prioritization", Definition = "Ranking options by importance and impact" },
            new() { Term = "Risk Assessment", Definition = "Evaluating potential negative outcomes before acting" },
            new() { Term = "Pre-mortem", Definition = "Exercise imagining failure causes before implementation" },
            new() { Term = "Ownership", Definition = "Clear assignment of responsibility for execution" },
            new() { Term = "Retrospective", Definition = "Review of outcomes to capture lessons learned" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 41, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "PSU", Definition = "Power Supply Unit converting AC to DC for components" },
            new() { Term = "80 PLUS", Definition = "Efficiency certification standard for power supplies" },
            new() { Term = "Airflow", Definition = "Directed movement of air to remove heat from components" },
            new() { Term = "Heat Sink", Definition = "Metal component dissipating heat from chips" },
            new() { Term = "Headroom", Definition = "Extra power and cooling capacity for stability and upgrades" }
        }});

        games.Add(new MiniGame { Id = gId++, LessonId = 42, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "RPO", Definition = "Recovery Point Objective — maximum acceptable data loss time window" },
            new() { Term = "RTO", Definition = "Recovery Time Objective — maximum acceptable service downtime duration" },
            new() { Term = "Snapshot", Definition = "Point-in-time copy used for fast recovery" },
            new() { Term = "Replication", Definition = "Copying data to another system for resilience" },
            new() { Term = "Restore Test", Definition = "Verification that backups can be recovered successfully" }
        }});

        // Lesson 43: File Systems
        games.Add(new MiniGame { Id = gId++, LessonId = 43, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "NTFS", Definition = "Default Windows file system supporting large files, permissions, journaling, and encryption." },
            new() { Term = "Journaling", Definition = "A file system feature that logs changes before applying them, enabling recovery after unexpected shutdowns." },
            new() { Term = "exFAT", Definition = "A cross-platform file system for removable drives supporting files larger than 4 GB with no size limit." },
            new() { Term = "Absolute path", Definition = "A full path starting from the root directory (e.g., C:\\Users\\file.txt or /home/user/file.txt)." },
            new() { Term = "ext4", Definition = "The default Linux file system — a mature, journaling-capable file system with strong performance and reliability." }
        }});

        // Lesson 44: Input and Output Devices
        games.Add(new MiniGame { Id = gId++, LessonId = 44, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "IPS panel", Definition = "A monitor panel type offering excellent color accuracy and wide viewing angles, preferred for design work." },
            new() { Term = "Plug-and-play", Definition = "The ability for an OS to automatically detect and install drivers for newly connected peripherals." },
            new() { Term = "Refresh rate", Definition = "How many times per second a monitor updates its image, measured in Hz. Higher rates reduce motion blur." },
            new() { Term = "Screen reader", Definition = "Assistive technology that reads on-screen content aloud to support visually impaired users." },
            new() { Term = "Touchscreen", Definition = "A display that acts as both an input device (detecting touches) and an output device (showing visuals)." }
        }});

        // Lesson 45: Computer Networks Overview
        games.Add(new MiniGame { Id = gId++, LessonId = 45, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Packet", Definition = "A small unit of data transmitted over a network. Large data is broken into packets and reassembled at the destination." },
            new() { Term = "NIC", Definition = "Network Interface Card — hardware that connects a device to a network and assigns it a unique MAC address." },
            new() { Term = "Router", Definition = "A network device that forwards IP packets between different networks, connecting a local network to the internet." },
            new() { Term = "TCP/IP", Definition = "The fundamental suite of protocols governing how data is transmitted and routed across the internet." },
            new() { Term = "Packet switching", Definition = "A network data transfer method where data is broken into packets that travel independently and are reassembled." }
        }});

        // Lesson 46: Software Types and Licenses
        games.Add(new MiniGame { Id = gId++, LessonId = 46, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Open-source software", Definition = "Software whose source code is publicly available under a license permitting viewing, modification, and redistribution." },
            new() { Term = "SaaS", Definition = "Software as a Service — software delivered over the internet without local installation (e.g., Google Docs, Adobe CC)." },
            new() { Term = "GPL", Definition = "GNU General Public License — an open-source license requiring derivatives to also be released as open source." },
            new() { Term = "System software", Definition = "Software that manages hardware and provides a foundation for applications, including the OS, drivers, and utilities." },
            new() { Term = "Freeware", Definition = "Software that is free to use but whose source code remains closed and proprietary." }
        }});

        // Lesson 47: Binary and Number Systems
        games.Add(new MiniGame { Id = gId++, LessonId = 47, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Binary (base-2)", Definition = "A number system using only 0 and 1. Each bit position represents a power of 2. Used internally by all computers." },
            new() { Term = "Byte", Definition = "8 bits. Can represent 256 values (0–255). The fundamental unit for measuring data storage." },
            new() { Term = "Hexadecimal (base-16)", Definition = "Uses digits 0–9 and A–F. Each hex digit represents exactly 4 bits. Used for memory addresses and color codes." },
            new() { Term = "Binary 1101 in decimal", Definition = "13. Calculated as 8+4+0+1 = 13." },
            new() { Term = "Bit", Definition = "The smallest unit of data in computing — either 0 or 1. From 'binary digit'." }
        }});

        // Lesson 48: Social Engineering Attacks
        games.Add(new MiniGame { Id = gId++, LessonId = 48, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Phishing", Definition = "Fraudulent emails impersonating trusted sources to steal credentials or install malware." },
            new() { Term = "Spear phishing", Definition = "A targeted phishing attack crafted with personalized information about the specific victim." },
            new() { Term = "Tailgating", Definition = "A physical social engineering attack where an unauthorized person follows an authorized one through a secured door." },
            new() { Term = "Pretexting", Definition = "Creating a fabricated scenario to manipulate a victim into revealing information or granting access." },
            new() { Term = "Vishing", Definition = "Voice phishing — social engineering conducted via phone calls to extract sensitive information." }
        }});

        // Lesson 49: Types of Malware
        games.Add(new MiniGame { Id = gId++, LessonId = 49, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Ransomware", Definition = "Malware that encrypts a victim's files and demands payment (ransom) for the decryption key." },
            new() { Term = "Worm", Definition = "Self-replicating malware that spreads across networks automatically without user interaction." },
            new() { Term = "Trojan", Definition = "Malware disguised as legitimate software that executes hidden malicious functions when run." },
            new() { Term = "Keylogger", Definition = "Malware that records keystrokes to capture passwords, credit card numbers, and sensitive information." },
            new() { Term = "Rootkit", Definition = "Malware that hides deep in the OS to maintain persistent, privileged, and hidden access." }
        }});

        // Lesson 50: Network Security Tools
        games.Add(new MiniGame { Id = gId++, LessonId = 50, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "IDS", Definition = "Intrusion Detection System — monitors traffic for attack signatures or anomalies and alerts administrators." },
            new() { Term = "IPS", Definition = "Intrusion Prevention System — detects and automatically blocks suspicious traffic in real time." },
            new() { Term = "NGFW", Definition = "Next-Generation Firewall — adds application awareness, deep packet inspection, and threat intelligence to traditional firewalls." },
            new() { Term = "SIEM", Definition = "Security Information and Event Management — aggregates logs from multiple sources for real-time correlation and alerting." },
            new() { Term = "Penetration testing", Definition = "Simulating real-world attacks to discover exploitable vulnerabilities before malicious actors do." }
        }});

        // Lesson 51: Security Policies and Compliance
        games.Add(new MiniGame { Id = gId++, LessonId = 51, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "PCI DSS", Definition = "Payment Card Industry Data Security Standard — governs the security of cardholder data environments." },
            new() { Term = "GDPR", Definition = "General Data Protection Regulation — EU regulation governing personal data collection, processing, and privacy rights." },
            new() { Term = "Data classification", Definition = "Labeling data by sensitivity level (public, internal, confidential, restricted) to define appropriate handling controls." },
            new() { Term = "Risk register", Definition = "A document recording identified risks, their likelihood, impact, and chosen mitigation controls." },
            new() { Term = "ISO 27001", Definition = "An international standard for information security management systems (ISMS)." }
        }});

        // Lesson 52: Incident Response
        games.Add(new MiniGame { Id = gId++, LessonId = 52, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "IR lifecycle", Definition = "Preparation → Identification → Containment → Eradication → Recovery → Lessons Learned." },
            new() { Term = "Containment", Definition = "The IR phase that isolates affected systems to prevent further spread of the incident." },
            new() { Term = "Eradication", Definition = "Removing all root causes of the incident — malware, compromised accounts, and exploited vulnerabilities." },
            new() { Term = "Tabletop exercise", Definition = "A simulated discussion-based exercise where IR team members walk through a hypothetical incident scenario." },
            new() { Term = "EDR", Definition = "Endpoint Detection and Response — security software providing deep visibility and response capabilities on individual endpoints." }
        }});

        // Lesson 53: Object-Oriented Programming
        games.Add(new MiniGame { Id = gId++, LessonId = 53, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Class", Definition = "A blueprint or template defining the attributes and methods that instances (objects) of that type will have." },
            new() { Term = "Inheritance", Definition = "An OOP mechanism where a child class inherits attributes and methods from a parent class." },
            new() { Term = "Encapsulation", Definition = "Bundling data and methods within a class and controlling access to internal state via public interfaces." },
            new() { Term = "__init__", Definition = "Python's constructor method, called automatically when a new object is created to initialize its attributes." },
            new() { Term = "Polymorphism", Definition = "The ability for different classes to be used through the same interface, responding to the same method differently." }
        }});

        // Lesson 54: File Handling in Python
        games.Add(new MiniGame { Id = gId++, LessonId = 54, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "open() mode 'w'", Definition = "Opens a file for writing. Creates the file if it doesn't exist and overwrites it if it does." },
            new() { Term = "with statement", Definition = "A context manager that ensures the file is automatically closed when the block exits, even if an error occurs." },
            new() { Term = "f.readlines()", Definition = "Reads all lines from a file and returns them as a list of strings." },
            new() { Term = "json.load()", Definition = "Parses a JSON file and returns the data as a Python dictionary or list." },
            new() { Term = "open() mode 'a'", Definition = "Opens a file in append mode — writes are added to the end without overwriting existing content." }
        }});

        // Lesson 55: Error Handling and Exceptions
        games.Add(new MiniGame { Id = gId++, LessonId = 55, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "try/except", Definition = "Python construct to catch and handle exceptions that occur in the try block." },
            new() { Term = "finally clause", Definition = "A block that always executes after try/except regardless of whether an exception occurred — used for cleanup." },
            new() { Term = "KeyError", Definition = "Raised when a dictionary key that doesn't exist is accessed." },
            new() { Term = "raise", Definition = "Python keyword used to intentionally trigger an exception: raise ValueError('Invalid input')." },
            new() { Term = "Custom exception", Definition = "A user-defined exception class created by subclassing Exception, allowing domain-specific error types." }
        }});

        // Lesson 56: Working with Libraries and pip
        games.Add(new MiniGame { Id = gId++, LessonId = 56, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "pip install", Definition = "Command to install a Python package from PyPI: pip install requests" },
            new() { Term = "Virtual environment", Definition = "An isolated Python environment for a project to prevent package version conflicts between projects." },
            new() { Term = "requirements.txt", Definition = "A file listing a project's dependencies and versions, created with 'pip freeze > requirements.txt'." },
            new() { Term = "PyPI", Definition = "Python Package Index — the official repository of open-source Python packages." },
            new() { Term = "pip freeze", Definition = "Lists all installed packages with exact versions — used to snapshot an environment for reproducibility." }
        }});

        // Lesson 57: Debugging and Testing
        games.Add(new MiniGame { Id = gId++, LessonId = 57, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "pytest", Definition = "The most popular Python testing framework — simple syntax, powerful plugins, and clear failure messages." },
            new() { Term = "TDD", Definition = "Test-Driven Development — write tests before code to define expected behavior, then implement to pass the tests." },
            new() { Term = "breakpoint()", Definition = "Python built-in (3.7+) that drops into the pdb debugger for interactive step-through execution." },
            new() { Term = "Code coverage", Definition = "Measures what percentage of source code is executed during tests. Tracked with coverage.py." },
            new() { Term = "Unit test", Definition = "A test that validates a single function or component in isolation from the rest of the system." }
        }});

        // Lesson 58: JavaScript DOM Manipulation
        games.Add(new MiniGame { Id = gId++, LessonId = 58, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "DOM", Definition = "Document Object Model — a tree representation of an HTML page that JavaScript can read and modify." },
            new() { Term = "querySelector()", Definition = "Selects the first DOM element matching a CSS selector: document.querySelector('.btn')" },
            new() { Term = "addEventListener()", Definition = "Attaches an event handler to an element: element.addEventListener('click', handler)" },
            new() { Term = "Event delegation", Definition = "Attaching one listener to a parent element to handle events from all its children via event bubbling." },
            new() { Term = "textContent", Definition = "A DOM property that sets or gets the plain text content of an element safely, without parsing HTML." }
        }});

        // Lesson 59: Responsive Web Design
        games.Add(new MiniGame { Id = gId++, LessonId = 59, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Flexbox", Definition = "A CSS layout model for one-dimensional layouts (rows or columns) with flexible sizing and alignment." },
            new() { Term = "CSS Grid", Definition = "A CSS layout system for two-dimensional layouts with explicit rows and columns." },
            new() { Term = "Media query", Definition = "@media (max-width: 768px) { } — applies CSS rules conditionally based on screen size or device features." },
            new() { Term = "Mobile-first design", Definition = "Designing for the smallest screen first, then adding complexity for larger screens using min-width media queries." },
            new() { Term = "Viewport meta tag", Definition = "<meta name='viewport' content='width=device-width, initial-scale=1'> — prevents mobile scaling of desktop layouts." }
        }});

        // Lesson 60: Web APIs and the Fetch API
        games.Add(new MiniGame { Id = gId++, LessonId = 60, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "REST API", Definition = "An API style using HTTP methods (GET, POST, PUT, DELETE) with stateless requests and typically JSON responses." },
            new() { Term = "HTTP 404", Definition = "Not Found — the server cannot locate the requested resource." },
            new() { Term = "fetch()", Definition = "JavaScript's built-in function for making HTTP requests: fetch(url).then(r => r.json()).then(data => ...)" },
            new() { Term = "CORS", Definition = "Cross-Origin Resource Sharing — a browser security policy restricting HTTP requests to different origins." },
            new() { Term = "HTTP 201", Definition = "Created — the server successfully created a new resource in response to a POST request." }
        }});

        // Lesson 61: Version Control with Git
        games.Add(new MiniGame { Id = gId++, LessonId = 61, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "git commit", Definition = "Saves a snapshot of staged changes with a descriptive message: git commit -m 'Add login feature'" },
            new() { Term = "git branch", Definition = "Creates or lists branches: git checkout -b feature/login creates and switches to a new branch." },
            new() { Term = "Pull request (PR)", Definition = "A request to review and merge a branch's changes into another branch, typically on GitHub/GitLab." },
            new() { Term = ".gitignore", Definition = "A file listing patterns of files Git should not track, such as build artifacts, node_modules, and .env files." },
            new() { Term = "git clone", Definition = "Creates a local copy of a remote repository including its full history." }
        }});

        // Lesson 62: Web Accessibility and Performance
        games.Add(new MiniGame { Id = gId++, LessonId = 62, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "WCAG", Definition = "Web Content Accessibility Guidelines — the international standard for making web content accessible to all users." },
            new() { Term = "LCP", Definition = "Largest Contentful Paint — a Core Web Vital measuring how long the largest visible content element takes to render." },
            new() { Term = "Alt text", Definition = "A text alternative for images, used by screen readers and displayed when images fail to load." },
            new() { Term = "Semantic HTML", Definition = "Using HTML elements for their intended meaning (<nav>, <button>, <h1>) rather than generic divs and spans." },
            new() { Term = "Lazy loading", Definition = "Deferring loading of off-screen images and resources until they are about to enter the viewport." }
        }});

        // Lesson 63: Articles and Determiners
        games.Add(new MiniGame { Id = gId++, LessonId = 63, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Definite article", Definition = "'The' — refers to a specific noun already known to both the speaker and listener." },
            new() { Term = "Indefinite article", Definition = "'A' or 'an' — introduces a non-specific noun for the first time. Use 'an' before vowel sounds." },
            new() { Term = "Zero article", Definition = "No article used — for uncountable nouns in general statements (Water is essential) and most proper nouns." },
            new() { Term = "Quantifiers", Definition = "Determiners expressing quantity: some, any, many, few, much, little, several, each, every." },
            new() { Term = "Rule: a vs. an", Definition = "Based on sound, not spelling. 'An hour' (silent h), 'a university' (starts with /j/ sound)." }
        }});

        // Lesson 64: Modal Verbs
        games.Add(new MiniGame { Id = gId++, LessonId = 64, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Modal + base infinitive", Definition = "Modals (can, must, should, etc.) are always followed by the base form of the verb without 'to'." },
            new() { Term = "Must (deduction)", Definition = "Expresses logical certainty about the present: 'He must be tired' means you are very sure he is tired." },
            new() { Term = "Should have + past participle", Definition = "A perfect modal expressing past regret or criticism: 'You should have called.' " },
            new() { Term = "May vs. might", Definition = "Both express possibility. 'May' is slightly more certain; 'might' is more tentative or hypothetical." },
            new() { Term = "Could (past ability)", Definition = "'Could' refers to past ability: 'I could swim when I was young.' Present ability uses 'can'." }
        }});

        // Lesson 65: Conditionals
        games.Add(new MiniGame { Id = gId++, LessonId = 65, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Zero conditional", Definition = "General truths: If/When + present simple, present simple. 'If you freeze water, it becomes ice.'" },
            new() { Term = "First conditional", Definition = "Real future possibilities: If + present simple, will + infinitive. 'If it rains, I'll stay home.'" },
            new() { Term = "Second conditional", Definition = "Unreal present/future: If + past simple, would + infinitive. 'If I won the lottery, I would travel.'" },
            new() { Term = "Third conditional", Definition = "Unreal past: If + past perfect, would have + past participle. 'If I had studied, I would have passed.'" },
            new() { Term = "Mixed conditional", Definition = "Combines 2nd and 3rd: If + past perfect (past condition), would + infinitive (present result)." }
        }});

        // Lesson 66: Passive Voice
        games.Add(new MiniGame { Id = gId++, LessonId = 66, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Passive voice formation", Definition = "Appropriate tense of 'be' + past participle: 'is written', 'was fixed', 'has been sent'." },
            new() { Term = "When to use passive", Definition = "When the doer is unknown, irrelevant, or when emphasizing the action/result over who performed it." },
            new() { Term = "Present continuous passive", Definition = "Is/are being + past participle: 'The building is being constructed.' " },
            new() { Term = "Active to passive", Definition = "Active: 'The chef cooked the meal.' Passive: 'The meal was cooked (by the chef).' " },
            new() { Term = "By-agent in passive", Definition = "The optional phrase indicating who performed the action: 'The report was written by Ana.' " }
        }});

        // Lesson 67: Reported Speech
        games.Add(new MiniGame { Id = gId++, LessonId = 67, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Tense backshift", Definition = "In reported speech tenses shift back: present → past, past → past perfect, will → would, can → could." },
            new() { Term = "Say vs. tell", Definition = "'Say' needs no object (He said he was late). 'Tell' requires a person (He told me he was late)." },
            new() { Term = "Reported yes/no question", Definition = "Uses if/whether: 'Are you ready?' → She asked if/whether I was ready." },
            new() { Term = "Reported command", Definition = "Uses tell/ask + object + infinitive: 'Sit down!' → She told him to sit down." },
            new() { Term = "Time expression shift", Definition = "Now → then, today → that day, tomorrow → the next day, here → there." }
        }});

        // Lesson 68: Spanish Pronouns
        games.Add(new MiniGame { Id = gId++, LessonId = 68, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Usted", Definition = "Formal 'you' singular in Spanish, used in professional, respectful, or unfamiliar contexts." },
            new() { Term = "Direct object pronouns", Definition = "Replace the noun receiving the action: me, te, lo/la, nos, os, los/las." },
            new() { Term = "Indirect object pronouns", Definition = "Indicate to/for whom: me, te, le, nos, os, les. Indirect precedes direct: Te lo doy." },
            new() { Term = "Reflexive pronoun 'se'", Definition = "Third-person reflexive pronoun for él/ella/usted/ellos: Se llama Ana (Her name is Ana)." },
            new() { Term = "Pronoun + affirmative command", Definition = "Pronouns attach to the end of affirmative commands: Dámelo (Give it to me)." }
        }});

        // Lesson 69: Present Tense Conjugation
        games.Add(new MiniGame { Id = gId++, LessonId = 69, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "-AR verb endings (present)", Definition = "hablo, hablas, habla, hablamos, habláis, hablan" },
            new() { Term = "-ER verb endings (present)", Definition = "como, comes, come, comemos, coméis, comen" },
            new() { Term = "Stem-changing e→ie", Definition = "Verbs like querer and entender change the stem vowel e→ie in all forms except nosotros/vosotros." },
            new() { Term = "Irregular yo: tener", Definition = "Tengo (I have) — irregular only in yo form, then follows regular -er pattern." },
            new() { Term = "Ir (to go) present tense", Definition = "voy, vas, va, vamos, vais, van — fully irregular, must be memorized." }
        }});

        // Lesson 70: Telling Time in Spanish
        games.Add(new MiniGame { Id = gId++, LessonId = 70, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Son las tres", Definition = "It's three o'clock. 'Son las' is used for all hours except one o'clock." },
            new() { Term = "Es la una", Definition = "It's one o'clock. Singular 'es la' is used only for one o'clock." },
            new() { Term = "Y media", Definition = "And a half — used to express the half hour: Son las seis y media (It's 6:30)." },
            new() { Term = "¿A qué hora?", Definition = "At what time? Used when asking when something happens: ¿A qué hora empieza?" },
            new() { Term = "De la tarde", Definition = "In the afternoon/evening (p.m.). De la mañana = a.m., de la noche = evening/night." }
        }});

        // Lesson 71: Spanish Prepositions
        games.Add(new MiniGame { Id = gId++, LessonId = 71, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Por vs. para", Definition = "Por: cause, duration, exchange, means. Para: purpose, destination, deadline, recipient." },
            new() { Term = "Del", Definition = "Mandatory contraction of 'de + el': Vengo del trabajo (I come from work)." },
            new() { Term = "Al", Definition = "Mandatory contraction of 'a + el': Voy al mercado (I go to the market)." },
            new() { Term = "Al lado de", Definition = "Next to / beside: La farmacia está al lado del banco." },
            new() { Term = "Sin", Definition = "Without: Café sin azúcar (coffee without sugar)." }
        }});

        // Lesson 72: Food and Restaurant Vocabulary
        games.Add(new MiniGame { Id = gId++, LessonId = 72, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "La cuenta, por favor", Definition = "The bill, please — standard phrase to request the check at a restaurant." },
            new() { Term = "Quisiera...", Definition = "I would like... — polite way to order food or request something in a restaurant." },
            new() { Term = "Picante", Definition = "Spicy. Not to be confused with caliente (hot in temperature)." },
            new() { Term = "Soy vegetariano/a", Definition = "I am vegetarian — essential dietary phrase for restaurant and travel situations." },
            new() { Term = "¿Qué me recomienda?", Definition = "What do you recommend? — useful for engaging with waitstaff and trying local specialties." }
        }});

        // Lesson 73: Network Devices Deep Dive
        games.Add(new MiniGame { Id = gId++, LessonId = 73, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Managed switch", Definition = "A switch supporting VLANs, QoS, port mirroring, and STP — configurable via CLI or web interface." },
            new() { Term = "Layer 3 switch", Definition = "A switch that can also perform routing between VLANs using IP addresses, combining switch and router functions." },
            new() { Term = "Proxy server", Definition = "An intermediary between clients and the internet providing caching, filtering, and anonymity." },
            new() { Term = "Load balancer", Definition = "Distributes incoming traffic across multiple servers to improve availability and prevent overload." },
            new() { Term = "STP", Definition = "Spanning Tree Protocol — prevents network loops by blocking redundant paths between switches." }
        }});

        // Lesson 74: Wireless Networking
        games.Add(new MiniGame { Id = gId++, LessonId = 74, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "802.11ax / Wi-Fi 6", Definition = "Latest major Wi-Fi standard offering up to 9.6 Gbps and improved performance in dense environments." },
            new() { Term = "WPA3", Definition = "Current Wi-Fi security standard providing stronger encryption and forward secrecy compared to WPA2." },
            new() { Term = "2.4 GHz vs. 5 GHz", Definition = "2.4 GHz: longer range, more congestion. 5 GHz: shorter range, higher speed, less interference." },
            new() { Term = "SSID", Definition = "Service Set Identifier — the visible name of a wireless network." },
            new() { Term = "Mesh Wi-Fi", Definition = "Multiple wireless nodes working together to provide seamless whole-home or office coverage." }
        }});

        // Lesson 75: DNS and DHCP
        games.Add(new MiniGame { Id = gId++, LessonId = 75, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "DNS A record", Definition = "Maps a domain name to its IPv4 address: example.com → 93.184.216.34" },
            new() { Term = "DHCP DORA", Definition = "The four-step DHCP process: Discover → Offer → Request → Acknowledge." },
            new() { Term = "DHCP reservation", Definition = "Binding a specific IP address to a device's MAC address so it always receives the same IP." },
            new() { Term = "TTL (DNS)", Definition = "Time to Live — how many seconds a DNS record is cached before re-querying the authoritative server." },
            new() { Term = "169.254.x.x address", Definition = "APIPA (Automatic Private IP Addressing) — self-assigned when DHCP fails, indicating a network configuration problem." }
        }});

        // Lesson 76: VPNs and Remote Access
        games.Add(new MiniGame { Id = gId++, LessonId = 76, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "WireGuard", Definition = "A modern VPN protocol with a minimal codebase, fast performance, and strong cryptography." },
            new() { Term = "Split tunneling", Definition = "VPN configuration routing only specific traffic (e.g., corporate) through the tunnel while other traffic goes direct." },
            new() { Term = "Site-to-site VPN", Definition = "A VPN connecting two entire networks (e.g., branch office to headquarters) over the internet." },
            new() { Term = "Zero trust", Definition = "A security model that verifies every access request regardless of network location, replacing broad VPN access." },
            new() { Term = "TLS/SSL VPN", Definition = "A VPN type that uses TLS for encryption, often accessible through a web browser without a dedicated client." }
        }});

        // Lesson 77: Network Monitoring and Troubleshooting
        games.Add(new MiniGame { Id = gId++, LessonId = 77, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "traceroute", Definition = "Maps the path packets take to a destination and shows per-hop latency, identifying where delays occur." },
            new() { Term = "Wireshark", Definition = "A graphical packet capture and analysis tool for deep inspection of network traffic at all protocol layers." },
            new() { Term = "SNMP", Definition = "Simple Network Management Protocol — allows network devices to report metrics to management systems." },
            new() { Term = "netstat / ss", Definition = "Shows active TCP/UDP connections and listening ports on a system." },
            new() { Term = "OSI-layer troubleshooting", Definition = "Start at Layer 1 (physical) and work up through Layer 2, 3, 4 to Layer 7 (application)." }
        }});

        // Lesson 78: HTTP and HTTPS In Depth
        games.Add(new MiniGame { Id = gId++, LessonId = 78, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "HTTP 200 OK", Definition = "The request succeeded. The response body contains the requested resource." },
            new() { Term = "TLS handshake", Definition = "The process where client and server negotiate encryption: certificate exchange → key agreement → session encryption." },
            new() { Term = "HTTP/2 multiplexing", Definition = "Allows multiple requests to be in flight simultaneously over a single TCP connection, reducing latency." },
            new() { Term = "HTTP 503", Definition = "Service Unavailable — the server is temporarily unable to handle requests, often due to overload or maintenance." },
            new() { Term = "HTTPS", Definition = "HTTP with TLS encryption — provides confidentiality, integrity, and authentication for web traffic." }
        }});

        // Lesson 79: DNS Protocol In Depth
        games.Add(new MiniGame { Id = gId++, LessonId = 79, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Recursive resolver", Definition = "A DNS server that performs iterative queries (root → TLD → authoritative) on behalf of the client." },
            new() { Term = "DNSSEC", Definition = "DNS Security Extensions — adds cryptographic signatures to DNS records to prevent cache poisoning." },
            new() { Term = "DNS over HTTPS (DoH)", Definition = "Encrypts DNS queries inside HTTPS traffic, preventing ISP snooping on your lookups." },
            new() { Term = "SOA record", Definition = "Start of Authority — contains zone metadata including serial number, refresh interval, and primary nameserver." },
            new() { Term = "PTR record", Definition = "Reverse DNS record mapping an IP address back to a hostname — used in email server verification." }
        }});

        // Lesson 80: DHCP Protocol
        games.Add(new MiniGame { Id = gId++, LessonId = 80, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "DHCPDISCOVER", Definition = "Broadcast message sent by a new network client searching for available DHCP servers." },
            new() { Term = "DHCP lease", Definition = "A temporary assignment of an IP address to a device for a configured duration." },
            new() { Term = "DHCP scope", Definition = "The defined pool of IP addresses a DHCP server can assign to clients." },
            new() { Term = "DHCP snooping", Definition = "A managed switch security feature that blocks rogue DHCP servers on untrusted ports." },
            new() { Term = "DHCP relay agent", Definition = "Forwards DHCP broadcast messages across routers to a centralized DHCP server on another subnet." }
        }});

        // Lesson 81: Email Protocols
        games.Add(new MiniGame { Id = gId++, LessonId = 81, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "SMTP port 587", Definition = "The standard port for email client submission with STARTTLS authentication." },
            new() { Term = "IMAP vs POP3", Definition = "IMAP keeps email on server and syncs across devices. POP3 downloads and typically deletes from server." },
            new() { Term = "SPF record", Definition = "A DNS TXT record listing IP addresses authorized to send email for a domain — reduces spoofing." },
            new() { Term = "DKIM", Definition = "DomainKeys Identified Mail — cryptographically signs email headers/body to verify message authenticity." },
            new() { Term = "DMARC", Definition = "Specifies what to do when SPF or DKIM fails: none (monitor), quarantine (junk), or reject." }
        }});

        // Lesson 82: Network Troubleshooting Tools
        games.Add(new MiniGame { Id = gId++, LessonId = 82, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "ping", Definition = "Sends ICMP echo requests to test whether a host is reachable and measures round-trip latency." },
            new() { Term = "nslookup / dig", Definition = "Query DNS servers to verify domain name resolution and inspect individual DNS records." },
            new() { Term = "arp -a", Definition = "Displays the ARP table showing the mapping of IP addresses to MAC addresses on the local network." },
            new() { Term = "curl", Definition = "A command-line tool for making HTTP/S requests — useful for testing web APIs and server connectivity." },
            new() { Term = "tcpdump", Definition = "A command-line packet capture tool for Linux/macOS — captures network traffic to a file for analysis." }
        }});

        // Lesson 83: Feature Engineering
        games.Add(new MiniGame { Id = gId++, LessonId = 83, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "StandardScaler", Definition = "Scales features to zero mean and unit variance — essential for distance-based and gradient algorithms." },
            new() { Term = "One-hot encoding", Definition = "Converts categorical variables into binary indicator columns — one column per category." },
            new() { Term = "Feature selection", Definition = "Removing irrelevant or redundant features to reduce noise, overfitting, and training time." },
            new() { Term = "L1 regularization", Definition = "Lasso regularization that can shrink feature coefficients to exactly zero, performing automatic feature selection." },
            new() { Term = "Log transform", Definition = "Applies logarithm to skewed features to reduce the effect of outliers and create more normal distributions." }
        }});

        // Lesson 84: Unsupervised Learning
        games.Add(new MiniGame { Id = gId++, LessonId = 84, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "K-Means", Definition = "A clustering algorithm assigning points to k clusters by minimizing within-cluster sum of squared distances." },
            new() { Term = "DBSCAN", Definition = "Density-Based Spatial Clustering — finds clusters of arbitrary shape based on point density, handles outliers." },
            new() { Term = "PCA", Definition = "Principal Component Analysis — reduces dimensionality by projecting data onto axes of maximum variance." },
            new() { Term = "t-SNE", Definition = "A dimensionality reduction technique for visualizing high-dimensional data in 2D or 3D." },
            new() { Term = "Anomaly detection", Definition = "Identifies data points that deviate significantly from normal patterns — used in fraud and intrusion detection." }
        }});

        // Lesson 85: Reinforcement Learning
        games.Add(new MiniGame { Id = gId++, LessonId = 85, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Policy (RL)", Definition = "The agent's strategy — a mapping from states to actions that the agent learns to maximize cumulative reward." },
            new() { Term = "Reward signal", Definition = "Immediate scalar feedback the agent receives from the environment after taking an action." },
            new() { Term = "Exploration vs exploitation", Definition = "Balancing trying new actions (exploration) vs. using known good actions (exploitation) during learning." },
            new() { Term = "Q-Learning", Definition = "A model-free RL algorithm that learns action-value (Q-value) functions to determine the best action in each state." },
            new() { Term = "DQN", Definition = "Deep Q-Network — combines Q-learning with deep neural networks to handle high-dimensional state spaces like pixels." }
        }});

        // Lesson 86: Model Deployment
        games.Add(new MiniGame { Id = gId++, LessonId = 86, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "MLOps", Definition = "Machine Learning Operations — applying DevOps practices (CI/CD, versioning, monitoring) to ML model development and deployment." },
            new() { Term = "Data drift", Definition = "When the distribution of input features in production shifts from what the model was trained on." },
            new() { Term = "ONNX", Definition = "Open Neural Network Exchange — a cross-framework format for exporting and sharing ML models." },
            new() { Term = "A/B testing (ML)", Definition = "Running two model versions simultaneously on live traffic to compare their real-world performance." },
            new() { Term = "FastAPI", Definition = "A modern Python web framework commonly used to deploy ML models as REST API endpoints." }
        }});

        // Lesson 87: Ethics in AI
        games.Add(new MiniGame { Id = gId++, LessonId = 87, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "AI bias", Definition = "When AI systems produce systematically unfair outcomes due to biased training data or model design." },
            new() { Term = "SHAP", Definition = "SHapley Additive exPlanations — an explainability method quantifying each feature's contribution to a prediction." },
            new() { Term = "Differential privacy", Definition = "A technique adding mathematically bounded noise to training data to prevent reconstruction of individual records." },
            new() { Term = "Hallucination (AI)", Definition = "AI models generating confident but factually incorrect or fabricated information." },
            new() { Term = "EU AI Act", Definition = "EU regulation categorizing AI systems by risk level and imposing requirements for high-risk applications." }
        }});

        // Lesson 88: Convolutional Neural Networks
        games.Add(new MiniGame { Id = gId++, LessonId = 88, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Convolution layer", Definition = "Slides learnable filters over the input to detect local patterns (edges, textures, shapes)." },
            new() { Term = "Max pooling", Definition = "Downsamples feature maps by keeping the maximum value in each window, reducing spatial size." },
            new() { Term = "ResNet", Definition = "A CNN architecture using residual (skip) connections enabling very deep networks (100+ layers) to train effectively." },
            new() { Term = "Feature map", Definition = "The output of applying a single filter to the input — highlights where that filter's pattern was detected." },
            new() { Term = "Transfer learning (CNNs)", Definition = "Reusing ImageNet-pretrained CNN weights as a starting point for a new image-related task." }
        }});

        // Lesson 89: Recurrent Neural Networks and LSTMs
        games.Add(new MiniGame { Id = gId++, LessonId = 89, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Vanishing gradient", Definition = "A training problem in deep networks where gradients become extremely small, preventing learning from distant timesteps." },
            new() { Term = "LSTM gates", Definition = "Forget gate (what to discard), input gate (what to add), output gate (what to pass forward)." },
            new() { Term = "GRU", Definition = "Gated Recurrent Unit — a simpler LSTM variant with two gates, achieving comparable performance with fewer parameters." },
            new() { Term = "Bidirectional RNN", Definition = "Processes a sequence in both forward and backward directions to capture context from both past and future." },
            new() { Term = "Hidden state (RNN)", Definition = "The internal memory vector passed from one timestep to the next, carrying accumulated context." }
        }});

        // Lesson 90: Transformers and Attention Mechanisms
        games.Add(new MiniGame { Id = gId++, LessonId = 90, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Self-attention", Definition = "Computes relationships between all token pairs in a sequence simultaneously using Query, Key, and Value vectors." },
            new() { Term = "Multi-head attention", Definition = "Runs multiple self-attention mechanisms in parallel to capture different types of relationships." },
            new() { Term = "Positional encoding", Definition = "Adds sequence position information to token embeddings since self-attention has no built-in notion of order." },
            new() { Term = "BERT", Definition = "Encoder-only Transformer pretrained bidirectionally — excels at understanding tasks (classification, Q&A)." },
            new() { Term = "GPT", Definition = "Decoder-only Transformer trained autoregressively — excels at text generation tasks." }
        }});

        // Lesson 91: Transfer Learning
        games.Add(new MiniGame { Id = gId++, LessonId = 91, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Fine-tuning", Definition = "Continuing training of a pretrained model on task-specific data, adapting weights to the new domain." },
            new() { Term = "Feature extraction", Definition = "Freezing pretrained layers and only training a new task-specific head on top of the fixed representations." },
            new() { Term = "Zero-shot learning", Definition = "A model performing a task it was never explicitly trained on by leveraging general pre-training knowledge." },
            new() { Term = "Few-shot learning", Definition = "Adapting a model to a new task with only a handful of labeled examples." },
            new() { Term = "ImageNet", Definition = "A large image dataset used to pretrain CNNs — ImageNet-pretrained weights are widely transferred to other vision tasks." }
        }});

        // Lesson 92: AI Safety and Alignment
        games.Add(new MiniGame { Id = gId++, LessonId = 92, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Alignment problem", Definition = "The challenge of ensuring AI systems pursue objectives truly aligned with human values and intentions." },
            new() { Term = "RLHF", Definition = "Reinforcement Learning from Human Feedback — trains models using human preference ratings to improve alignment." },
            new() { Term = "Prompt injection", Definition = "A jailbreaking technique where malicious instructions in user input override the AI system's intended behavior." },
            new() { Term = "Mechanistic interpretability", Definition = "Research reverse-engineering what specific circuits in neural networks do — to detect misaligned behavior." },
            new() { Term = "Constitutional AI", Definition = "Anthropic's alignment technique using a set of principles to guide AI self-critique and revision." }
        }});

        // Lesson 93: Email Etiquette
        games.Add(new MiniGame { Id = gId++, LessonId = 93, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "BCC", Definition = "Blind Carbon Copy — recipients cannot see each other's addresses, protecting privacy in group emails." },
            new() { Term = "Reply All", Definition = "Sends your response to all recipients of the original email — use only when all truly need your reply." },
            new() { Term = "Effective subject line", Definition = "Specific, informative, and actionable — tells recipients what the email is about at a glance." },
            new() { Term = "Email sign-off", Definition = "A closing phrase before your name: 'Best regards', 'Thanks', or 'Sincerely' depending on formality." },
            new() { Term = "ALL CAPS in email", Definition = "Perceived as shouting — avoid in professional emails as it conveys aggression or urgency inappropriately." }
        }});

        // Lesson 94: Giving and Receiving Feedback
        games.Add(new MiniGame { Id = gId++, LessonId = 94, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "SBI model", Definition = "Situation – Behavior – Impact: a framework for giving objective, specific, actionable feedback." },
            new() { Term = "Specific feedback", Definition = "Feedback that describes exactly what was observed and its impact, not vague praise or criticism." },
            new() { Term = "Receiving feedback well", Definition = "Listen without interrupting, ask clarifying questions, separate emotional reaction from useful information." },
            new() { Term = "'I' statements", Definition = "Framing feedback around your own observation and impact rather than making character judgments." },
            new() { Term = "Feedback culture", Definition = "An environment where regular, honest feedback flows in all directions without fear of blame or punishment." }
        }});

        // Lesson 95: Conflict Resolution
        games.Add(new MiniGame { Id = gId++, LessonId = 95, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Collaborating (TKI)", Definition = "The conflict mode that is both assertive and cooperative — seeks solutions satisfying all parties' interests." },
            new() { Term = "Interest-based resolution", Definition = "Focusing on parties' underlying needs rather than their stated positions to find creative solutions." },
            new() { Term = "Mediator", Definition = "A neutral third party who facilitates structured dialogue to help conflicting parties reach their own resolution." },
            new() { Term = "Avoiding (TKI)", Definition = "Withdrawing from or postponing conflict — sometimes appropriate, but often leads to escalation if overused." },
            new() { Term = "Emotional self-regulation", Definition = "Managing your emotional state during conflict to prevent escalation and keep focus on resolution." }
        }});

        // Lesson 96: Cross-Cultural Communication
        games.Add(new MiniGame { Id = gId++, LessonId = 96, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "High-context culture", Definition = "Communication relying heavily on implicit meaning, relationships, and nonverbal cues (e.g., Japan, China)." },
            new() { Term = "Low-context culture", Definition = "Communication where meaning is explicit in words, with preference for direct, clear statements (e.g., US, Germany)." },
            new() { Term = "Power distance", Definition = "Hofstede dimension measuring acceptance of hierarchical inequality within a culture." },
            new() { Term = "Cultural Intelligence (CQ)", Definition = "The ability to adapt communication and work style effectively across different cultural contexts." },
            new() { Term = "Individualism vs. collectivism", Definition = "Whether a culture prioritizes individual achievement (US) or group harmony and loyalty (Japan, many Asian cultures)." }
        }});

        // Lesson 97: Negotiation Skills
        games.Add(new MiniGame { Id = gId++, LessonId = 97, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "BATNA", Definition = "Best Alternative to a Negotiated Agreement — your fallback option if no deal is reached. Knowing it is your greatest source of leverage." },
            new() { Term = "Anchoring", Definition = "The first offer sets the psychological reference point for the entire negotiation — make a principled first move." },
            new() { Term = "Interest-based negotiation", Definition = "Focuses on underlying needs and motivations rather than fixed positions, opening creative solution space." },
            new() { Term = "Value creation", Definition = "Finding solutions that expand the total value for both parties before dividing it — avoid zero-sum thinking." },
            new() { Term = "Getting to Yes", Definition = "Fisher & Ury's landmark book introducing principled (interest-based) negotiation as an alternative to positional bargaining." }
        }});

        // Lesson 98: Building High-Performing Teams
        games.Add(new MiniGame { Id = gId++, LessonId = 98, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Psychological safety", Definition = "Team members feeling safe to speak up, disagree, and make mistakes without fear of punishment — #1 team success predictor." },
            new() { Term = "Project Aristotle", Definition = "Google research finding psychological safety as the single strongest predictor of team effectiveness." },
            new() { Term = "Tuckman's model", Definition = "Team development stages: Forming → Storming → Norming → Performing → Adjourning." },
            new() { Term = "Groupthink", Definition = "A dysfunction where teams converge on consensus without critical evaluation — psychological safety counters it." },
            new() { Term = "Retrospective", Definition = "A structured team meeting after a milestone to reflect on what went well, what didn't, and what to improve." }
        }});

        // Lesson 99: Delegation and Empowerment
        games.Add(new MiniGame { Id = gId++, LessonId = 99, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Delegation", Definition = "Assigning responsibility for a task or decision to a team member with appropriate authority and context." },
            new() { Term = "Empowerment", Definition = "Giving team members genuine decision-making authority within defined boundaries, fostering ownership." },
            new() { Term = "Micromanagement", Definition = "Over-controlling delegated work — undermines the person, reduces autonomy, and defeats delegation's purpose." },
            new() { Term = "Situational leadership", Definition = "Adapting leadership style (support/direction) based on a team member's current competence and confidence." },
            new() { Term = "Trust building through delegation", Definition = "Start with lower-stakes tasks, provide feedback, and increase responsibility as competence and confidence grow." }
        }});

        // Lesson 100: Performance Management
        games.Add(new MiniGame { Id = gId++, LessonId = 100, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "SMART goals", Definition = "Specific, Measurable, Achievable, Relevant, Time-bound — a framework for setting clear, actionable goals." },
            new() { Term = "PIP", Definition = "Performance Improvement Plan — a structured plan defining the performance gap, expected improvement, support, and timeline." },
            new() { Term = "Continuous feedback", Definition = "Regular, ongoing feedback throughout the year rather than reserving all feedback for annual reviews." },
            new() { Term = "Recognition", Definition = "Timely, specific acknowledgment of contributions — drives retention and motivation in high performers." },
            new() { Term = "Annual review", Definition = "A formal performance discussion that should hold no surprises — documenting what has already been discussed." }
        }});

        // Lesson 101: Change Management
        games.Add(new MiniGame { Id = gId++, LessonId = 101, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Kotter's 8 steps", Definition = "Create urgency → Coalition → Vision → Enlist → Enable → Short-term wins → Sustain → Institute." },
            new() { Term = "ADKAR", Definition = "Awareness, Desire, Knowledge, Ability, Reinforcement — individual change management model by Prosci." },
            new() { Term = "Change resistance", Definition = "Normal human response to change driven by fear of unknown, loss of control, or distrust of leadership." },
            new() { Term = "Change sponsor", Definition = "A senior leader visibly championing the change — the single most important success factor in most change efforts." },
            new() { Term = "Change fatigue", Definition = "Exhaustion from too many simultaneous changes — requires careful sequencing and pacing of organizational changes." }
        }});

        // Lesson 102: Strategic Thinking
        games.Add(new MiniGame { Id = gId++, LessonId = 102, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "SWOT analysis", Definition = "Framework assessing internal Strengths and Weaknesses alongside external Opportunities and Threats." },
            new() { Term = "OKRs", Definition = "Objectives and Key Results — a goal-setting framework connecting strategic direction to measurable team outcomes." },
            new() { Term = "PESTLE analysis", Definition = "Political, Economic, Social, Technological, Legal, Environmental — macro-environment scanning framework." },
            new() { Term = "Scenario planning", Definition = "Developing strategic responses to multiple plausible future scenarios rather than betting on one forecast." },
            new() { Term = "Strategic vs. tactical", Definition = "Strategic thinking asks 'why' and 'where are we going?' Tactical execution focuses on 'how' to achieve it." }
        }});

        // Lesson 103: Peripherals and Connectivity
        games.Add(new MiniGame { Id = gId++, LessonId = 103, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "USB4", Definition = "Latest USB standard offering up to 40 Gbps over USB-C, compatible with Thunderbolt 3/4." },
            new() { Term = "Thunderbolt 4", Definition = "Intel interface over USB-C providing 40 Gbps data, dual 4K video, daisy-chaining, and 100W charging." },
            new() { Term = "DisplayPort 2.1", Definition = "A video interface standard supporting up to 8K resolution and higher refresh rates than HDMI 2.1." },
            new() { Term = "HDMI 2.1", Definition = "Supports up to 4K@144Hz and 8K@30Hz — the current standard for gaming monitors and TVs." },
            new() { Term = "USB-C", Definition = "A reversible connector shape (not a speed standard) that can carry USB data, Thunderbolt, video, and power." }
        }});

        // Lesson 104: PC Building Process
        games.Add(new MiniGame { Id = gId++, LessonId = 104, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Thermal paste", Definition = "A compound applied between CPU and cooler to fill microscopic gaps and maximize heat transfer." },
            new() { Term = "XMP / EXPO", Definition = "BIOS memory profiles activating RAM's rated speed. XMP = Intel, EXPO = AMD." },
            new() { Term = "PCIe x16 slot", Definition = "The primary expansion slot for discrete GPUs, providing maximum bandwidth for graphics processing." },
            new() { Term = "M.2 NVMe", Definition = "A compact, high-speed SSD form factor plugging directly into the motherboard, bypassing SATA bottlenecks." },
            new() { Term = "Front-panel headers", Definition = "Motherboard connectors for the case's power button, reset button, and LED indicators." }
        }});

        // Lesson 105: Overclocking and Tuning
        games.Add(new MiniGame { Id = gId++, LessonId = 105, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Intel K-series CPU", Definition = "Unlocked Intel processors (e.g., i7-14700K) with adjustable multipliers that support overclocking." },
            new() { Term = "XMP profile", Definition = "An Intel memory overclock profile enabling DDR4/DDR5 kits to run at their rated speed above JEDEC defaults." },
            new() { Term = "MSI Afterburner", Definition = "A popular GPU overclocking utility for adjusting core clock, memory clock, and power limits." },
            new() { Term = "Prime95", Definition = "A CPU stress-testing application used to verify stability of CPU overclocks under sustained full load." },
            new() { Term = "Core voltage (Vcore)", Definition = "CPU supply voltage — increasing it stabilizes higher overclocks but significantly increases heat output." }
        }});

        // Lesson 106: PC Troubleshooting
        games.Add(new MiniGame { Id = gId++, LessonId = 106, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "POST beep codes", Definition = "Audio signals from the BIOS during Power-On Self-Test that identify specific hardware failures." },
            new() { Term = "BSOD", Definition = "Blue Screen of Death — Windows fatal error screen displaying a stop code that identifies the failure type." },
            new() { Term = "CrystalDiskInfo", Definition = "A tool that reads S.M.A.R.T. data from drives to assess health and predict potential failures." },
            new() { Term = "MemTest86", Definition = "A standalone memory diagnostic tool that exhaustively tests RAM for errors outside the operating system." },
            new() { Term = "Safe Mode", Definition = "Windows boot mode loading only essential drivers — if issues disappear, a driver or startup program is the cause." }
        }});

        // Lesson 107: Laptop vs Desktop
        games.Add(new MiniGame { Id = gId++, LessonId = 107, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Thermal throttling", Definition = "Automatic CPU/GPU clock reduction when temperatures exceed safe thresholds — more common in laptops than desktops." },
            new() { Term = "Ultrabook", Definition = "A thin, light laptop category prioritizing battery life, portability, and design over maximum performance." },
            new() { Term = "Docking station", Definition = "Connects a laptop to external monitors, keyboard, mouse, and peripherals via a single cable for a desktop experience." },
            new() { Term = "Framework laptop", Definition = "A modular, repairable laptop designed with user-replaceable components as an exception to soldered designs." },
            new() { Term = "Mini-PC", Definition = "Compact desktop computers (Intel NUC, Mac mini) bridging the gap between laptop portability and desktop performance." }
        }});

        // Lesson 108: Cloud Storage Services
        games.Add(new MiniGame { Id = gId++, LessonId = 108, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Amazon S3", Definition = "Amazon's object storage service — the most widely used cloud storage with 11 nines of durability." },
            new() { Term = "Client-side encryption", Definition = "Encrypting files before upload so only the key holder can decrypt them, even if the provider is compromised." },
            new() { Term = "Data sovereignty", Definition = "Legal requirements dictating which country or jurisdiction data must remain in — a key cloud storage consideration." },
            new() { Term = "11 nines durability", Definition = "99.999999999% durability — achieved through redundant replication across multiple availability zones." },
            new() { Term = "OneDrive", Definition = "Microsoft's cloud storage service, integrated with Windows and Office 365, offering 5 GB free storage." }
        }});

        // Lesson 109: Network Attached Storage
        games.Add(new MiniGame { Id = gId++, LessonId = 109, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "NAS", Definition = "Network Attached Storage — a dedicated storage device connected to a local network for centralized file access." },
            new() { Term = "SMB/CIFS", Definition = "Server Message Block — the standard Windows file sharing protocol, supported by all major NAS systems." },
            new() { Term = "Synology DSM", Definition = "DiskStation Manager — Synology's NAS operating system known for its polished user interface." },
            new() { Term = "Plex Media Server", Definition = "Media server software that runs on NAS to stream your video, music, and photo collection to any device." },
            new() { Term = "NFS", Definition = "Network File System — the standard Linux/Unix network file sharing protocol, supported by enterprise NAS devices." }
        }});

        // Lesson 110: Storage Area Networks
        games.Add(new MiniGame { Id = gId++, LessonId = 110, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "SAN", Definition = "Storage Area Network — a dedicated high-speed network providing servers with block-level access to shared storage." },
            new() { Term = "iSCSI", Definition = "Transports SCSI block storage commands over standard IP/Ethernet — a lower-cost alternative to Fibre Channel." },
            new() { Term = "Fibre Channel", Definition = "A dedicated SAN fabric using specialized switches and HBAs — the traditional high-performance enterprise SAN technology." },
            new() { Term = "Thin provisioning", Definition = "Presenting a server with a larger logical volume than physically exists — space is allocated as data is actually written." },
            new() { Term = "NVMe-oF", Definition = "NVMe over Fabrics — extends NVMe performance over Ethernet or InfiniBand for the fastest networked storage access." }
        }});

        // Lesson 111: Data Deduplication and Compression
        games.Add(new MiniGame { Id = gId++, LessonId = 111, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Lossless compression", Definition = "Compression that reconstructs data exactly — required for executables, databases, and code. Examples: ZIP, ZSTD, LZ4." },
            new() { Term = "Lossy compression", Definition = "Compression that discards imperceptible data for much higher ratios — JPEG, MP3, H.265. Not for exact-data use." },
            new() { Term = "Deduplication", Definition = "Eliminates redundant data blocks across stored files, storing only one copy and referencing it for duplicates." },
            new() { Term = "Inline deduplication", Definition = "Processes data for duplicates as it arrives — immediate space savings at higher real-time CPU cost." },
            new() { Term = "ZSTD", Definition = "Zstandard — a modern lossless compression algorithm balancing excellent compression ratio and high speed." }
        }});

        // Lesson 112: Object Storage and Modern Architectures
        games.Add(new MiniGame { Id = gId++, LessonId = 112, Type = MiniGameType.Flashcard, Items = new()
        {
            new() { Term = "Object storage", Definition = "Stores data as objects (data + metadata + unique ID) in flat namespaces called buckets, accessed via HTTP API." },
            new() { Term = "Bucket", Definition = "The container in object storage that holds objects — analogous to a top-level directory but with a flat namespace." },
            new() { Term = "S3 API", Definition = "Amazon S3's HTTP API — the de facto standard for object storage, implemented by Azure Blob, GCS, MinIO, and Ceph." },
            new() { Term = "Erasure coding", Definition = "A data protection technique splitting data into fragments and distributing parity across locations, enabling recovery from multiple failures." },
            new() { Term = "Data lake", Definition = "A centralized repository storing vast amounts of raw data in object storage for analytics and ML processing." }
        }});

        return games;
    }
}
