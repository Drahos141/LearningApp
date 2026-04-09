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
                                Content = "A computer is an electronic device that processes data according to instructions stored in its memory.\n\nComputers consist of hardware and software. Hardware includes physical components like the CPU, RAM, storage, and input/output devices. Software includes the operating system and applications that tell the hardware what to do.\n\nThe core of every computer is the Central Processing Unit (CPU), which performs calculations and executes instructions. Memory (RAM) temporarily stores data that the CPU is currently using, while long-term storage (HDD/SSD) keeps data persistently.\n\nComputers communicate with users through input devices (keyboard, mouse) and output devices (monitor, speakers). Together, these components create a system capable of performing complex tasks at high speed." },
                            new() { Id = lessonId++, SubcategoryId = 1, Order = 2, Title = "Operating Systems",
                                Content = "An operating system (OS) is the software that manages computer hardware and provides a platform for running applications.\n\nPopular operating systems include Windows, macOS, and Linux. The OS handles memory management, process scheduling, file systems, and device drivers.\n\nThe kernel is the core component of the OS — it directly manages hardware resources. On top of the kernel sits the shell and graphical user interface (GUI), which let users interact with the system.\n\nKey OS concepts include multitasking (running multiple programs simultaneously), file permissions, and virtual memory (using disk space as extra RAM)." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 1, Name = "Cybersecurity",
                        Description = "Understand how to protect systems and data from threats.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 2, Order = 1, Title = "Introduction to Cybersecurity",
                                Content = "Cybersecurity is the practice of protecting computer systems, networks, and data from digital attacks, theft, and damage.\n\nThe three pillars of security are Confidentiality (keeping data private), Integrity (ensuring data is not tampered with), and Availability (making sure systems are accessible when needed). These are known as the CIA Triad.\n\nCommon threats include malware (viruses, worms, ransomware), phishing attacks (tricking users into revealing credentials), and denial-of-service attacks (overwhelming a server to take it offline).\n\nSecurity professionals use tools like firewalls, antivirus software, intrusion detection systems, and encryption to defend against these threats." },
                            new() { Id = lessonId++, SubcategoryId = 2, Order = 2, Title = "Encryption and Passwords",
                                Content = "Encryption transforms readable data (plaintext) into an unreadable format (ciphertext) using an algorithm and a key. Only someone with the correct key can decrypt and read the data.\n\nSymmetric encryption uses the same key for both encryption and decryption (e.g., AES). Asymmetric encryption uses a public key to encrypt and a private key to decrypt (e.g., RSA), which is the basis for HTTPS.\n\nStrong passwords are at least 12 characters long and include a mix of uppercase letters, lowercase letters, numbers, and symbols. Password managers help users store and generate strong, unique passwords for every service.\n\nTwo-factor authentication (2FA) adds an extra layer of security by requiring a second verification step beyond just a password." }
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
                                Content = "In Python, variables are used to store data. Unlike many languages, Python is dynamically typed — you don't need to declare the type of a variable explicitly.\n\nBasic data types include integers (int), floating-point numbers (float), strings (str), and booleans (bool). For example: age = 25, name = 'Alice', is_active = True.\n\nPython also has collection types: lists (ordered, mutable), tuples (ordered, immutable), sets (unordered, unique items), and dictionaries (key-value pairs).\n\nYou can check a variable's type with the type() function. Python variables are case-sensitive, so 'name' and 'Name' are different variables." },
                            new() { Id = lessonId++, SubcategoryId = 3, Order = 2, Title = "Control Flow",
                                Content = "Control flow determines the order in which code executes. Python uses indentation (spaces or tabs) to define code blocks.\n\nConditional statements use if, elif, and else keywords. For example: if age >= 18: print('Adult') elif age >= 13: print('Teen') else: print('Child').\n\nLoops allow repeating code. The for loop iterates over a sequence: for item in list. The while loop repeats as long as a condition is true: while count < 10.\n\nBreak exits a loop early, continue skips to the next iteration, and pass is a no-op placeholder. List comprehensions provide a concise way to create lists: squares = [x**2 for x in range(10)]." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 2, Name = "Web Development",
                        Description = "Build websites with HTML, CSS, and JavaScript.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 4, Order = 1, Title = "HTML Basics",
                                Content = "HTML (HyperText Markup Language) is the standard language for creating web pages. It structures content using elements represented by tags.\n\nEvery HTML page has a basic structure: the DOCTYPE declaration, an html root element, a head (with metadata like title and CSS links), and a body (with visible content).\n\nCommon tags include headings (h1–h6), paragraphs (p), links (a href), images (img src), lists (ul, ol, li), and divs (div) for grouping content. Semantic tags like header, nav, main, section, article, and footer give meaning to the structure.\n\nAttributes provide additional information: class and id for CSS/JavaScript targeting, href for links, src for images, and alt for accessibility." },
                            new() { Id = lessonId++, SubcategoryId = 4, Order = 2, Title = "CSS Styling",
                                Content = "CSS (Cascading Style Sheets) controls the visual appearance of HTML elements. Styles can be applied inline, in a style tag, or in an external .css file.\n\nSelectors target HTML elements: element selectors (p {}), class selectors (.classname {}), ID selectors (#id {}), and attribute selectors. The cascade determines which rule wins when multiple rules apply to the same element.\n\nThe box model describes how every element occupies space: content, padding, border, and margin from inside out. Flexbox and Grid are modern layout systems — Flexbox is one-dimensional, Grid is two-dimensional.\n\nMedia queries enable responsive design: @media (max-width: 768px) { ... } applies styles only on small screens." }
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
                                Content = "English has 12 main tenses that express time and aspect. The three main time frames are past, present, and future, each with simple, continuous, perfect, and perfect continuous forms.\n\nSimple tenses describe facts and habits: 'She walks to school.' Continuous tenses describe actions in progress: 'She is walking to school.' Perfect tenses describe completed actions with relevance to another time: 'She has walked to school.'\n\nThe most commonly used tenses are: Simple Present (I eat), Simple Past (I ate), Simple Future (I will eat), Present Continuous (I am eating), Present Perfect (I have eaten), and Past Perfect (I had eaten).\n\nChoosing the correct tense is crucial for clear communication. Context clues like time expressions (yesterday, now, by tomorrow) help determine which tense to use." },
                            new() { Id = lessonId++, SubcategoryId = 5, Order = 2, Title = "Parts of Speech",
                                Content = "Every word in English belongs to a part of speech that describes its function in a sentence. The eight main parts of speech are nouns, pronouns, verbs, adjectives, adverbs, prepositions, conjunctions, and interjections.\n\nNouns name people, places, things, or ideas. Pronouns replace nouns (he, she, it, they). Verbs express actions or states of being. Adjectives describe nouns (beautiful, large). Adverbs modify verbs, adjectives, or other adverbs (quickly, very).\n\nPrepositions show relationships between elements (in, on, at, by). Conjunctions connect words, phrases, or clauses (and, but, or, because). Interjections express emotions (Oh!, Wow!).\n\nUnderstanding parts of speech helps with sentence structure, writing clearly, and learning grammar rules." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 3, Name = "Spanish Basics",
                        Description = "Start learning Spanish vocabulary and essential phrases.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 6, Order = 1, Title = "Greetings and Introductions",
                                Content = "Spanish greetings vary by time of day and formality. Common greetings include: Hola (Hello), Buenos días (Good morning), Buenas tardes (Good afternoon), and Buenas noches (Good evening/night).\n\nTo introduce yourself say: Me llamo... (My name is...) or Soy... (I am...). To ask someone's name: ¿Cómo te llamas? (informal) or ¿Cómo se llama usted? (formal).\n\nUseful phrases: ¿Cómo estás? (How are you? – informal), ¿Cómo está usted? (formal), Bien, gracias (Fine, thank you), Mucho gusto (Nice to meet you), Encantado/a (Pleased to meet you).\n\nSpanish has two forms of 'you': tú (informal, for friends and family) and usted (formal, for strangers or elders). This affects verb conjugation throughout the language." },
                            new() { Id = lessonId++, SubcategoryId = 6, Order = 2, Title = "Numbers and Colors",
                                Content = "Learning numbers and colors is essential for everyday communication in Spanish.\n\nNumbers 1–10: uno, dos, tres, cuatro, cinco, seis, siete, ocho, nueve, diez. 11–15: once, doce, trece, catorce, quince. Tens: veinte (20), treinta (30), cuarenta (40), cincuenta (50), cien (100).\n\nBasic colors: rojo (red), azul (blue), verde (green), amarillo (yellow), negro (black), blanco (white), naranja (orange), morado/violeta (purple), rosa (pink), gris (grey), marrón/café (brown).\n\nIn Spanish, adjectives (including colors) agree in gender and number with the noun they describe. For example: el coche rojo (the red car, masculine), la casa roja (the red house, feminine), los coches rojos (the red cars, masculine plural)." }
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
                                Content = "A computer network is a collection of interconnected devices that can share resources and communicate with each other. Networks range from small home setups to the global internet.\n\nTypes of networks by size: LAN (Local Area Network) covers a small area like a home or office. WAN (Wide Area Network) covers large geographic areas. MAN (Metropolitan Area Network) covers a city. The Internet is a global network of networks.\n\nNetwork devices include: routers (connect different networks), switches (connect devices within a network), access points (provide Wi-Fi), and modems (convert signals for internet access).\n\nNetworks use two main models: client-server (clients request services from dedicated servers) and peer-to-peer (devices share directly without a central server)." },
                            new() { Id = lessonId++, SubcategoryId = 7, Order = 2, Title = "IP Addressing",
                                Content = "Every device on a network is identified by an IP address. IPv4 addresses consist of four octets separated by dots (e.g., 192.168.1.1), with a range from 0 to 255 in each octet, providing about 4.3 billion unique addresses.\n\nIPv6 uses 128-bit addresses written in hexadecimal (e.g., 2001:0db8:85a3::8a2e:0370:7334) to solve the address exhaustion problem of IPv4.\n\nSubnet masks divide IP addresses into network and host portions. For example, a mask of 255.255.255.0 (/24) means the first 24 bits identify the network and the last 8 bits identify individual hosts.\n\nPrivate IP ranges (not routable on the internet): 192.168.x.x, 172.16–31.x.x, and 10.x.x.x. NAT (Network Address Translation) allows multiple devices to share one public IP." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 4, Name = "TCP/IP Protocols",
                        Description = "Dive into the protocols that power the internet.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 8, Order = 1, Title = "The OSI Model",
                                Content = "The OSI (Open Systems Interconnection) model is a conceptual framework that describes how network communication works in 7 layers.\n\nLayer 7 – Application: User-facing protocols (HTTP, FTP, DNS, SMTP). Layer 6 – Presentation: Data translation, encryption (SSL/TLS). Layer 5 – Session: Managing sessions between applications. Layer 4 – Transport: Reliable data transfer (TCP, UDP). Layer 3 – Network: Logical addressing and routing (IP). Layer 2 – Data Link: Physical addressing (MAC), error detection (Ethernet). Layer 1 – Physical: Bits over the wire (cables, signals).\n\nA helpful mnemonic: 'All People Seem To Need Data Processing' (Application → Physical) or 'Please Do Not Throw Sausage Pizza Away' (Physical → Application).\n\nEach layer communicates with the layer directly above and below it, adding or removing headers as data moves through the stack." },
                            new() { Id = lessonId++, SubcategoryId = 8, Order = 2, Title = "TCP vs UDP",
                                Content = "TCP (Transmission Control Protocol) and UDP (User Datagram Protocol) are the two main transport-layer protocols.\n\nTCP is connection-oriented: it establishes a connection via a three-way handshake (SYN, SYN-ACK, ACK), guarantees delivery, ensures packets arrive in order, and performs error checking and retransmission. Used for web browsing (HTTP/HTTPS), email, and file transfers where reliability matters.\n\nUDP is connectionless: it sends packets without establishing a connection, offers no delivery guarantee or ordering, but is much faster with lower overhead. Used for video streaming, online gaming, DNS lookups, and VoIP where speed matters more than perfect reliability.\n\nCommon port numbers: HTTP 80, HTTPS 443, FTP 21, SSH 22, DNS 53, SMTP 25. A socket is a combination of IP address and port (e.g., 192.168.1.1:443)." }
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
                                Content = "Machine learning (ML) is a subset of artificial intelligence where systems learn from data to improve their performance on tasks without being explicitly programmed.\n\nThere are three main types of ML: Supervised learning (the model learns from labeled examples — input/output pairs), Unsupervised learning (the model finds patterns in unlabeled data), and Reinforcement learning (an agent learns by interacting with an environment and receiving rewards or penalties).\n\nThe typical ML workflow: collect and prepare data → choose a model → train the model → evaluate performance → tune hyperparameters → deploy.\n\nCommon applications include image recognition, spam detection, recommendation systems, fraud detection, and natural language processing. Python libraries like scikit-learn, TensorFlow, and PyTorch are widely used." },
                            new() { Id = lessonId++, SubcategoryId = 9, Order = 2, Title = "Supervised Learning",
                                Content = "Supervised learning trains a model on labeled data — each training example has an input (features) and a known output (label). The model learns to map inputs to outputs and then predicts labels for new, unseen data.\n\nTwo main tasks: Classification (predicting a category, e.g., spam/not spam, cat/dog) and Regression (predicting a continuous value, e.g., house price, temperature).\n\nCommon algorithms: Linear Regression (for regression), Logistic Regression (for classification despite the name), Decision Trees, Random Forests, Support Vector Machines (SVM), and k-Nearest Neighbors (kNN).\n\nKey concepts: Training set (data used to train), Test set (data used to evaluate), Overfitting (model learns training data too well, performs poorly on new data), Underfitting (model is too simple to capture patterns), and Cross-validation (technique to assess generalizability)." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 5, Name = "Neural Networks",
                        Description = "Explore deep learning and neural network architectures.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 10, Order = 1, Title = "How Neural Networks Work",
                                Content = "A neural network is a computational model inspired by the human brain. It consists of layers of interconnected nodes (neurons) that process information.\n\nLayers: The Input layer receives raw data. Hidden layers perform transformations (a deep network has multiple hidden layers). The Output layer produces the final prediction.\n\nEach connection has a weight that is adjusted during training. Each neuron applies an activation function (like ReLU or Sigmoid) to introduce non-linearity, enabling the network to learn complex patterns.\n\nTraining uses backpropagation: the network makes a prediction, calculates the error using a loss function, then propagates the error backward to update weights using an optimizer like Gradient Descent. This process repeats over many iterations (epochs) until the model converges." },
                            new() { Id = lessonId++, SubcategoryId = 10, Order = 2, Title = "Deep Learning Applications",
                                Content = "Deep learning (DL) uses neural networks with many layers to automatically learn hierarchical representations of data. It has transformed many fields.\n\nConvolutional Neural Networks (CNNs) are specialized for image data — they use convolution operations to detect features like edges, shapes, and textures. Used in image classification, object detection, and medical imaging.\n\nRecurrent Neural Networks (RNNs) and LSTMs process sequential data (text, speech, time series). Transformers have largely replaced RNNs for NLP tasks — they power large language models like GPT and BERT.\n\nGenerative AI includes GANs (Generative Adversarial Networks) for creating images, and diffusion models for image/video generation. Large Language Models (LLMs) can generate text, write code, translate languages, and answer questions." }
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
                                Content = "Effective communication is the ability to convey information clearly, listen actively, and adapt your message to your audience. It is one of the most sought-after skills in the workplace.\n\nThe communication process involves a sender, message, medium, receiver, and feedback. Barriers like noise, assumptions, language differences, and emotional state can distort messages.\n\nActive listening means fully concentrating on the speaker — not just hearing words but understanding meaning. Techniques: maintain eye contact, nod, ask clarifying questions, and paraphrase to confirm understanding.\n\nNon-verbal communication (body language, facial expressions, tone of voice, gestures) can reinforce or contradict verbal messages. Studies suggest non-verbal cues carry a large portion of meaning in face-to-face interactions." },
                            new() { Id = lessonId++, SubcategoryId = 11, Order = 2, Title = "Public Speaking",
                                Content = "Public speaking is the ability to communicate ideas effectively to a group. It is consistently rated as one of the most valuable professional skills.\n\nPreparing a speech: Know your audience, define your purpose (inform, persuade, entertain), structure your content (opening, body, conclusion), and practice out loud multiple times.\n\nDelivery techniques: Speak clearly and at a moderate pace, use pauses for emphasis, vary your tone and volume, maintain eye contact with different parts of the audience, and use gestures naturally.\n\nManaging nerves: Deep breathing calms the nervous system. Reframe anxiety as excitement. Visualize success. The more you speak publicly, the more comfortable you become — consistent practice is the key.\n\nThe opening and closing are most memorable. Start with a hook (story, question, surprising fact) and end with a memorable call to action or summary." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 6, Name = "Leadership",
                        Description = "Learn what makes an effective leader and how to inspire teams.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 12, Order = 1, Title = "Leadership Styles",
                                Content = "Leadership style is how a leader directs, motivates, and manages people. Different styles suit different situations and team types.\n\nAutocratic leadership: The leader makes decisions unilaterally with little input from team members. Effective in crisis situations or when quick decisions are needed, but can reduce morale long-term.\n\nDemocratic (Participative) leadership: The leader involves team members in decision-making. Increases buy-in, creativity, and morale, but can be slow.\n\nTransformational leadership: The leader inspires and motivates through a compelling vision and personal example. Effective for driving innovation and change.\n\nServant leadership: The leader puts the team's needs first, removes obstacles, and develops people. Associated with high trust and long-term performance.\n\nSituational leadership theory suggests effective leaders adapt their style based on the competence and commitment level of each team member." },
                            new() { Id = lessonId++, SubcategoryId = 12, Order = 2, Title = "Emotional Intelligence",
                                Content = "Emotional intelligence (EI or EQ) is the ability to recognize, understand, manage, and use emotions — both your own and others' — effectively.\n\nDaniel Goleman's EI model has five components: Self-awareness (knowing your emotions), Self-regulation (managing your emotions), Motivation (intrinsic drive to achieve), Empathy (understanding others' feelings), and Social skills (managing relationships).\n\nHigh EQ leaders build stronger teams, handle conflict better, stay calm under pressure, and inspire trust. EQ is often cited as a stronger predictor of leadership success than IQ.\n\nDeveloping EI: Practice mindfulness to increase self-awareness. Keep a journal to reflect on emotional reactions. Seek feedback. Practice empathy by actively trying to see situations from others' perspectives. Pause before reacting in emotionally charged situations." }
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
                                Content = "The CPU (Central Processing Unit) is the brain of the computer. It executes instructions and performs calculations. Key specifications include clock speed (GHz), number of cores, cache size, and architecture.\n\nModern CPUs have multiple cores (2, 4, 8, 16+) allowing true parallel processing. Hyper-threading (Intel) / SMT (AMD) allows each core to handle two threads simultaneously. The cache (L1, L2, L3) provides ultra-fast temporary storage near the processor.\n\nThe motherboard is the main circuit board that connects all components. It contains the CPU socket, RAM slots, PCIe slots (for GPU and other expansion cards), M.2 slots (for NVMe SSDs), SATA ports, and I/O ports. The chipset manages communication between the CPU and other components.\n\nThe BIOS/UEFI firmware initializes hardware during startup and provides settings for the system before the OS loads." },
                            new() { Id = lessonId++, SubcategoryId = 13, Order = 2, Title = "RAM and GPU",
                                Content = "RAM (Random Access Memory) is the short-term memory of the computer. It temporarily stores data that the CPU is actively using. More RAM allows more programs to run simultaneously without slowdown.\n\nRAM types: DDR4 and DDR5 are current standards. Key specs are capacity (GB), speed (MHz/MT/s), and latency (CL timings). RAM is volatile — data is lost when power is removed.\n\nThe GPU (Graphics Processing Unit) was originally designed for rendering graphics but is now also used for AI, scientific computing, and cryptocurrency mining. A GPU contains thousands of smaller cores optimized for parallel tasks.\n\nDedicated GPUs (discrete) have their own VRAM (video memory). Integrated graphics share system RAM and are built into the CPU or motherboard. For gaming and machine learning, a powerful dedicated GPU is essential.\n\nThermal management (heatsinks, fans, liquid cooling) is critical for both CPU and GPU to prevent thermal throttling." }
                        }
                    },
                    new()
                    {
                        Id = subId++, CategoryId = 7, Name = "Storage Technologies",
                        Description = "Explore different types of storage and how they work.",
                        Lessons = new List<Lesson>
                        {
                            new() { Id = lessonId++, SubcategoryId = 14, Order = 1, Title = "HDD vs SSD",
                                Content = "Hard Disk Drives (HDDs) store data on spinning magnetic platters read by a mechanical arm. They offer large capacities at low cost but are slower and more fragile due to moving parts. Typical speeds: 80–160 MB/s.\n\nSolid State Drives (SSDs) store data in NAND flash memory chips with no moving parts. They are much faster, more durable, lighter, and quieter but cost more per gigabyte.\n\nSSD types: SATA SSDs use the same connector as HDDs (up to ~550 MB/s). NVMe SSDs connect via the PCIe bus (M.2 slot) and reach 3,000–7,000 MB/s (Gen 3–5).\n\nHDDs are still used where large, low-cost storage is needed (backup drives, NAS, servers). SSDs are standard for OS and application drives. Many systems use both — SSD for speed and HDD for bulk storage.\n\nWear leveling and TRIM are features of SSDs that distribute writes evenly and maintain performance over time." },
                            new() { Id = lessonId++, SubcategoryId = 14, Order = 2, Title = "RAID and Backup Strategies",
                                Content = "RAID (Redundant Array of Independent Disks) combines multiple physical drives into one logical unit for performance or redundancy.\n\nRAID 0 (Striping): Data split across drives for maximum performance. No redundancy — if one drive fails, all data is lost. RAID 1 (Mirroring): Data duplicated on two drives. If one fails, the other takes over. RAID 5: Data striped across 3+ drives with parity, tolerating one drive failure. RAID 6: Like RAID 5 but tolerates two drive failures. RAID 10: Combines mirroring and striping — fast and redundant but requires 4+ drives.\n\nRAID is not a backup — it protects against hardware failure but not accidental deletion, ransomware, or site disasters. The 3-2-1 backup rule: keep 3 copies of data, on 2 different media types, with 1 copy offsite (e.g., cloud).\n\nCloud storage (Google Drive, OneDrive, Backblaze) provides offsite backups with automatic syncing." }
                        }
                    }
                }
            }
        };

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

        return games;
    }
}
