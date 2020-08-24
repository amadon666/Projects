 //Узел дерева
        class Node<T>
        {
            public char Symbol { get; set; }
            public T Data { get; set; }
            public bool IsWord { get; set; }
            public string Prefix { get; set; }

            public Dictionary<char, Node<T>> SubNodes { get; set; }
                   
            public Node(char symbol, T data, string prefix)
            {
                Symbol = symbol;
                Data = data;
                SubNodes = new Dictionary<char, Node<T>>();
                Prefix = prefix;
            }

            public override string ToString()
            {
                return $"{Data} [{SubNodes.Count}] ({Prefix})";
            }

            public override bool Equals(object obj)
            {
                if (obj is Node<T> item)
                {
                    return Data.Equals(item);
                }
                else
                {
                    return false;
                }
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public Node<T> TryFind(char symbol)
            {
                if(SubNodes.TryGetValue(symbol, out Node<T> value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }
        //Префиксное дерево
        class Trie<T>
        {
            private Node<T> root;
            public int Count { get; set; }
            public Trie()
            {
                root = new Node<T>('\0', default(T),"");
                Count = 1;
            }
            public void Add(string key, T data)
            {
                AddNode(key,data,root);        
            }

            private void AddNode(string key, T data, Node<T> node)
            {
                if (String.IsNullOrEmpty(key))
                {
                    if (!node.IsWord) {
                        node.Data = data;
                        node.IsWord = true;
                    }
                }
                else
                {
                    var symbol = key[0];
                    var subNode = node.TryFind(symbol);
                    if (subNode != null)
                    {
                        AddNode(key.Substring(1), data, subNode);
                    }
                    else
                    {
                        var newNode = new Node<T>(key[0], data, node.Prefix + key[0]);
                        node.SubNodes.Add(key[0], newNode);
                        AddNode(key.Substring(1),data, newNode);
                    }
                }            
            }

            public void Remove(string key)
            {
                RemoveNode(key, root);
            }

            private void RemoveNode(string key, Node<T> node)
            {
                if (String.IsNullOrEmpty(key))
                {
                    if (node.IsWord)
                    {
                        node.IsWord = false;
                    }
                }
                else
                {
                    var subnode = node.TryFind(key[0]);
                    if(subnode != null)
                    {
                        RemoveNode(key.Substring(1), subnode);
                    }
                    
                }
            }

            public bool TrySearch(string key, out T value)
            {
                return SearchNode(key, root, out value);
            }

            private bool SearchNode(string key, Node<T> node, out T value)
            {
                value = default(T);
                var result = false;
                if (String.IsNullOrEmpty(key))
                {
                    if (node.IsWord)
                    {
                        value = node.Data;
                        result = true;
                    }
                }
                else
                {
                    var subnode = node.TryFind(key[0]);
                    if (subnode != null)
                    {
                        result = SearchNode(key.Substring(1), subnode, out value);
                    }          
                }
                return result;
            }
        }

        static void Main(string[] args)
        {
            var trie = new Trie<int>();
            trie.Add("привет", 50);
            trie.Add("мир", 100);
            trie.Add("приз", 200);
            trie.Add("мирный", 50);
            trie.Add("подарок", 100);
            trie.Add("проект", 200);
            trie.Add("прапорщик", 50);
            trie.Add("правый", 100);
            trie.Add("год", 200);
            trie.Add("герой", 50);
            trie.Add("голубь", 100);
            trie.Add("прокрастинация", 1000);

            trie.Remove("правый");
            trie.Remove("мир");

            Srh(trie, "привет");
            Srh(trie, "мир");
            Srh(trie, "мирный");
            Srh(trie, "привет");
            Srh(trie, "прокрастинация");
            Srh(trie, "год");
            Console.ReadKey();
        }

        private static void Srh(Trie<int> trie, string word)
        {
            if (trie.TrySearch(word, out int value))
            {
                Console.WriteLine(word + " " + value);
            }
            else
            {
                Console.WriteLine("Не найдено " + word);
            }
        }
