#pragma once
#include<stdio.h>
#include<cstringt.h>

template<typename T>
class Vector
{
private :
	unsigned int	_size		= 0;
	unsigned int	_capacity	= 0;
	T*				_container	= nullptr;

public:
	Vector() {};
	~Vector() { if (nullptr != _container) delete[] _container; };

	T operator [] (unsigned int pos)
	{
		if (_size <= pos)
		{
			return static_cast<T>(NULL);
		}

		return _container[pos];
	}

	unsigned int GetSize() { return _size; };

	unsigned int GetCapacity() { return _capacity; };

	T At(unsigned int pos) {
		if (pos < _size) return *(_container + pos);
		else return static_cast<T>(NULL);
	};

	void PushBack(T data)
	{
		if (nullptr == _container)
		{
			_capacity++;
			T* container = new T[_capacity]();
			container[0] = data;
			_container = container;

			_size++;
			return;
		}

		if (_size != _capacity)		//빈공간이 있는 경우
		{
			_container[_size] = data;
			_size++;
		}
		else						//꽉찬경우
		{
			_capacity *= 2;
			Resize(_capacity);

			_container[_size] = data;
			_size++;
		}
	};

	T	PopBack() {
		if (0 == _size)
		{
			return static_cast<T>(NULL);
		}

		_size--;
		
		return *(_container + _size);
	};

	bool IsEmpty()
	{
		return (_size == 0)? true : false;
	}

	void Clear()
	{
		delete[] _container;
		_size		= 0;
		_capacity	= 0;
	}

	void Resize(int size)
	{
		_capacity = size;
		T* newContainer = new T[_capacity]();
		memset(newContainer, 0, sizeof(T) * _capacity);

		memcpy_s(newContainer, sizeof(T) * _capacity, _container, sizeof(T) * _capacity);

		//기존의 컨테이너 제거
		if (nullptr != _container) delete[] _container;

		_container = newContainer;
	}
};

